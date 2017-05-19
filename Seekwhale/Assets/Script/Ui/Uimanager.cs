using System;
using DG.Tweening;
using UnityEngine.UI;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Uimanager
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Uimanager : Singleton<Uimanager>
    {
        public GameObject root;
        public GameObject tipsviewprefab;
        public List<Baseview> views = new List<Baseview>();

        public Categroylist categroy;
        public Itemlist itemlist;
        public Dictionary<string, GameObject> categroygos = new Dictionary<string, GameObject>();
        //public Dictionary<string, Itemsetup> itemsgos = new Dictionary<string, Itemsetup>();
        public List<Itemsetup> items = new List<Itemsetup>();
        [SerializeField] internal List<GameObject> tips = new List<GameObject>();
        private Dictionary<string, Baseview> viewdictionary = new Dictionary<string, Baseview>();
        private IEnumerator coroutine;

        private float progress;
        private Scanview scanview;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Start()
        {
            coroutine = Updatedownloadprogress();
            foreach (Baseview view in views)
            {
                Addviewtodictioary(view.viewid, view);
            }
            views.Clear();
            views = null;


            Takephotoorrecord.Getinstance().Takephoto.doingbefore = Ontakephoto;
            Takephotoorrecord.Getinstance().Takephoto.doingafter = Ontakedphoto;
        }

        /// <summary>
        /// 添加UI界面到dictioary表里面
        /// </summary>
        /// <param name="_viewid"></param>
        /// <param name="_view"></param>
        public void Addviewtodictioary(string _viewid, Baseview _view)
        {
            if (!viewdictionary.ContainsKey(_viewid))
                viewdictionary.Add(_viewid, _view);
        }

        /// <summary>
        /// 获取viewid的ui界面
        /// </summary>
        /// <param name="_viewid"></param>
        /// <returns></returns>
        public Baseview Getviewfromviewid(string _viewid)
        {
            Baseview mbaseview = null;
            if (viewdictionary.ContainsKey(_viewid))
            {
                viewdictionary.TryGetValue(_viewid, out mbaseview);
                return mbaseview;
            }
            return null;
        }

        /// <summary>
        /// 打开UI界面
        /// </summary>
        /// <param name="_viewid"></param>
        public void Openview(string _viewid)
        {
            if (Getviewfromviewid(_viewid) == null) return;
            Baseview bview = Getviewfromviewid(_viewid);

            GameObject targetview = bview.gameObject;
            targetview.SetActive(true);
            //bview.Updateviewstatus();
        }

        /// <summary>
        /// 关闭UI界面
        /// </summary>
        /// <param name="_viewid"></param>
        public void Closeview(string _viewid)
        {
            if (Getviewfromviewid(_viewid) == null) return;
            Baseview bview = Getviewfromviewid(_viewid);

            GameObject targetview = bview.gameObject;
            // bview.Updateviewstatus();
        }

        /// <summary>
        /// 显示进度条
        /// </summary>
        public void Showprogress()
        {
            if (SKassetbundlehelper.instance.bundlewww == null)
                return;
            scanview = Getscanview();
            //scanview.Updateviewstatus();
            if (coroutine != null)
                StartCoroutine(coroutine);
            else
            {
                coroutine = Updatedownloadprogress();
                StartCoroutine(coroutine);
            }
        }

        /// <summary>
        /// 隐藏进度条 
        /// </summary>
        public void Hideprogress()
        {
            progress = 0;
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            scanview = Getscanview();
            if (scanview == null) return;
            scanview.progress.gameObject.SetActive(false);
            scanview.progress.value = 0;

            //scanview.Updateviewstatus();
        }



        /// <summary>
        /// 显示提示
        /// </summary>
        /// <param name="msg"></param>
        internal void Showtips(string msg)
        {
            GameObject tmp = Instantiate(tipsviewprefab, this.transform);
            tmp.transform.localScale = Vector3.one;
            tmp.transform.localRotation = Quaternion.identity;
            tmp.transform.localPosition = Vector3.zero;
            tips.Add(tmp);
            RectTransform rect = tmp.GetComponent<RectTransform>();
            tmp.GetComponent<RectTransform>().offsetMax = new Vector2(0, 68.6f);
            tmp.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            tmp.GetComponent<Tipsview>().Setupmsg(msg);

            if (Scannermanager.Getinstance().Getcurrecostatus != Scanstatus.STOPSCANNING ||
                !Scannermanager.Getinstance().isinited)
                Hideprogress();

            //((Tipsview)Getviewfromviewid(typeof(Tipsview).Name)).Setupmsg(msg);
        }

        /// <summary>
        /// 创建资源item
        /// </summary>
        /// <param name="_json"></param>
        internal void Getitems(string _json)
        {
            Itemlist tmp = Browser.Getinstance().Getdatafromjson<Itemlist>(_json);
            if (itemlist.result == null) return;
            int count = tmp.result.Count - itemlist.result.Count;
            if (count > 0)
            {
                Showcaseview showcase = Getviewfromviewid(typeof (Showcaseview).Name) as Showcaseview;
                for (int i = 0; i < tmp.result.Count; i++)
                {
                    itemlist.result.Add(tmp.result[i]);
                    GameObject tmpgo = Uiprefabcreator.Uicreator(null, showcase.horizontalprefab);
                    Itemsetup cs = tmpgo.GetComponent<Itemsetup>();
                    cs.unitid = tmp.result[i].trackerid;
                    cs.item = itemlist.result[i];
                    items.Add(cs);
                }

                for (int i = 0; i < itemlist.result.Count; i++)
                {
                    //set parent
                    GameObject categroy_root = null;
                    categroygos.TryGetValue(itemlist.result[i].category, out categroy_root);
                    GameObject item = null;
                    items[i].transform.SetParent(categroy_root.transform.GetComponent<Categorysetup>().itemroot);
                    items[i].transform.localScale = Vector3.one;
                    items[i].transform.localPosition = Vector3.zero;

                    items[i].title.text = itemlist.result[i].itemname;
                    items[i].unitid = itemlist.result[i].trackerid;
                    Browser.Getinstance()
                        .GetMarkless(itemlist.result[i].thumbnails, Setthumbnails, null, items[i].background);
                }
            }
        }

        internal void Addblackfade(bool _fade, Action _callback)
        {
            GameObject fadegameobject = new GameObject("Fade");
            Image image = fadegameobject.AddComponent<Image>();
            image.color = Color.clear;

            RectTransform rect = null;
            rect = !fadegameobject.GetComponent<RectTransform>()
                ? fadegameobject.AddComponent<RectTransform>()
                : fadegameobject.GetComponent<RectTransform>();

            rect.SetParent(this.transform.root);
            rect.anchoredPosition3D = Vector3.zero;
            rect.localScale = Vector3.one;

            rect.sizeDelta = new Vector2(Screen.width, Screen.height);
            if (_fade)
            {
                Viewop();
                Tweener tw = image.DOFade(1, 0.1f);
                tw.OnComplete(() =>
                {

                    Tweener tw2 = image.DOFade(0, 1f);
                    tw2.OnComplete(() =>
                    {
                        Destroy(fadegameobject, 0.5f);
                    });
                    tw2.SetDelay(0.5f);
                    if (_callback != null)
                        _callback.Invoke();
                });

                tw.OnStart(() =>
                {
                    image.color = Color.black;
                });
            }
        }


        private void Viewop()
        {
            //界面之间的逻辑切换
            Baseview scanview = Uimanager.Getinstance().Getviewfromviewid(typeof (Scanview).Name);
            Baseview caseview = Uimanager.Getinstance().Getviewfromviewid(typeof (Showcaseview).Name);
            Baseview detailview = Uimanager.Getinstance().Getviewfromviewid(typeof (Detaildescriptionview).Name);

            //Uistack.Getinstance().Openview(caseview, Viewstatus.HIDE, caseview.viewid);
            //Uistack.Getinstance().Openview(detailview, Viewstatus.HIDE, detailview.viewid);
            //Uistack.Getinstance().Openview(scanview, Viewstatus.SHOW, scanview.viewid);
        }


        /// <summary>
        /// 更新进度条
        /// </summary>
        /// <returns></returns>
        private IEnumerator Updatedownloadprogress()
        {
            while (true)
            {
                yield return null;
                if (SKassetbundlehelper.instance.bundlewww == null)
                    yield break;
                progress = SKassetbundlehelper.instance.bundlewww.progress;

                scanview = Getscanview();
                scanview.progress.value = progress;
            }
        }


        /// <summary>
        /// 获取扫描界面
        /// </summary>
        /// <returns></returns>
        private Scanview Getscanview()
        {
            if (scanview == null)
            {
                Baseview bv;
                viewdictionary.TryGetValue(typeof (Scanview).Name, out bv);
                return scanview = ((Scanview) bv);
            }
            return scanview;
        }



        private IEnumerator Waittoload()
        {
            yield return new WaitForSeconds(0.25f);
            Getitems();
        }

        private void Getitems()
        {
            Browser.Getinstance().Getitemlist(Getitems, Showtips);
        }

        private void Setthumbnails(byte[] _thumbnails, object _object)
        {
            Texture2D t2d = new Texture2D(128, 128, TextureFormat.RGB24, false);
            t2d.LoadImage(_thumbnails);

            Image thumbnails = (Image) (_object);
            thumbnails.sprite = Sprite.Create(t2d, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f), 100, 0,
                SpriteMeshType.FullRect);
        }

        private void Ontakephoto()
        {
            root.gameObject.SetActive(false);
        }

        private void Ontakedphoto()
        {
            root.gameObject.SetActive(true);
            StartCoroutine(Waittoshowtips("照片已存储到相册"));
        }


        private void Update()
        {
            if (Takephotoorrecord.Getinstance().Recording == null)
                return;
            Takephotoorrecord.Getinstance().Recording.Recordtimeupdate();

//            if (Input.GetMouseButtonUp(0))
//            {
//                Auxiliaryfun.Getinstance().Focuson();
//            }
        }

        public IEnumerator Waittoshowtips(string _msg)
        {
            yield return new WaitForSeconds(0.25f);
            Showtips(_msg);
        }


        internal void Getcategory(string _json)
        {
            Categroylist tmp = Browser.Getinstance().Getdatafromjson<Categroylist>(_json);

            if (categroy.result == null) return;

            //计算服务器返回数据的长度
            int count = tmp.result.Count - categroy.result.Count;

            if (count > 0)
            {
                Showcaseview showcase = Getviewfromviewid(typeof (Showcaseview).Name) as Showcaseview;
                for (int i = 0; i < count; i++)
                {
                    categroy.result.Add(new Categroy());
                }

                for (int i = 0; i < categroy.result.Count; i++)
                {
                    categroy.result[i] = tmp.result[i];
                    Categorysetup cs =
                        Uiprefabcreator.Uicreator(showcase.verticalroot, showcase.verticalprefab)
                            .GetComponent<Categorysetup>();
                    cs.title.text = categroy.result[i].alias;
                    categroygos.Add(categroy.result[i].category, cs.gameObject);
                }
            }
            StartCoroutine(Waittoload());
        }
    }
}
