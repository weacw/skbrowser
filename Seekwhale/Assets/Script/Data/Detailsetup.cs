using System.IO;
using DG.Tweening;
using Mono.Data.Sqlite;
using UnityEngine.UI;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Detailsetup
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Detailsetup : MonoBehaviour
    {
        public Item item;

        public Text title;
        public Text description;
        public Text downloadtext;

        public Button favorite;
        public Button markless;
        public Button download;
        public Button videoplayerbtn;

        public RawImage videopayer;




        public GameObject markerlessview;
        public Image markerlessimage;
        public Image downloadprogress;

        public Button markerlessclosebtn;
        public Button markerlesscdownloadbtn;


        private MediaPlayerCtrl mediaplayer;
        private Texture2D tmpthumbnails;

        private bool isdownloading;


        private void Start()
        {
            if (mediaplayer == null)
                mediaplayer = FindObjectOfType<MediaPlayerCtrl>();

            Btneventbind();
        }



        /// <summary>
        /// 对按钮添加相应事件
        /// </summary>
        public void OnEnable()
        {

            description.text = item.description;
            title.text = item.itemname;

            if (mediaplayer != null)
            {
                mediaplayer.Load(item.tutourl);
                mediaplayer.Pause();
            }

            //设置下载按钮文字，若是已缓存至本地则为false，反之
            if (!SKassetbundlehelper.instance.Checkcache(item.Getitemurl(), item.version))
            {
                downloadtext.text = "<color=#3266BAFF>点击下载</color>";
                downloadprogress.fillAmount = 0;
            }
            else
            {
                downloadtext.text = "<color=white>进入体验</color>";
                downloadprogress.fillAmount = 1;
            }

            if (item != null)
                Browser.Getinstance().GetMarkless(item.tutorthumbnail, Setuptutorthumbnail, null, videopayer);
#if VERSION2_0
            Sqlitehelper helper = new Sqlitehelper("data source=skbrowser.db");
            SqliteDataReader sdr = helper.Readfromid("favorite_test", "id", item.id.ToString());
            if (sdr.HasRows)
            {
                favorite.GetComponent<Image>().sprite = Resources.Load<Sprite>("Ui/btn_like_sel");
            }
            else
            {
                favorite.GetComponent<Image>().sprite = Resources.Load<Sprite>("Ui/btn_like_nor");
            }
            helper.CloseSqlConnection();
#endif
        }

        /// <summary>
        /// 设置缩略图
        /// </summary>
        /// <param name="_bytes"></param>
        /// <param name="_target"></param>
        private void Setuptutorthumbnail(byte[] _bytes, object _target)
        {
            tmpthumbnails = new Texture2D(256, 256, TextureFormat.ARGB32, false);
            tmpthumbnails.LoadImage(_bytes);
            ((RawImage)_target).texture = tmpthumbnails;
        }

        /// <summary>
        /// 清空所有对按钮的事件添加
        /// </summary>
        public void OnDisable()
        {
            //Remove evnet 
            favorite.onClick.RemoveAllListeners();
            markless.onClick.RemoveAllListeners();
            download.onClick.RemoveAllListeners();
            videoplayerbtn.onClick.RemoveAllListeners();
            markerlessclosebtn.onClick.RemoveAllListeners();
            markerlesscdownloadbtn.onClick.RemoveAllListeners();

            if (Scannermanager.Getinstance().onparsingitemend != null)
                Scannermanager.Getinstance().onparsingitemend = null;

            //clean cache
            mediaplayer.m_strFileName = null;
            mediaplayer.Stop();
            mediaplayer.UnLoad();
            DestroyImmediate(videopayer.texture);
            videoplayerbtn.gameObject.SetActive(true);


            //set favorite to null
            //favorite.GetComponent<Image>().sprite = Resources.Load<Sprite>("Ui/btn_like_nor");
        }

        /// <summary>
        /// 下载模型资源
        /// </summary>
        private void Downloadasset()
        {

            if (isdownloading)
            {
                Uimanager.Getinstance().Showtips("资源正在下载中");
                return;
            }
            //检查资源是否有缓存
            if (!SKassetbundlehelper.instance.Checkcache(item.Getitemurl(), item.version))
            {
                isdownloading = true;
                //检查网络是否可用
                if (!Browser.Getinstance().GetNetreachable(false))
                {
                    return;
                }
                //载入资源
                StartCoroutine(SKassetbundlehelper.instance.Paringbundle(item.Getitemurl(), item.version, Ondownloaded));
            }
            else
            {
                //由showcase界面下载进入到scan view 解析模型资源
                StartCoroutine(SKassetbundlehelper.instance.Paringbundle(item.Getitemurl(), item.version,
                      Scannermanager.Getinstance().Parsingbundlecallback));

                //黑色浸入
                //Uimanager.Getinstance().Addblackfade(true,null);
                //Viewop();
                // Viewop();
            }
        }

        private void Viewop()
        {
            //界面之间的逻辑切换
            Baseview scanview = Uimanager.Getinstance().Getviewfromviewid(typeof(Scanview).Name);
            Baseview caseview = Uimanager.Getinstance().Getviewfromviewid(typeof(Showcaseview).Name);
            Baseview detailview = Uimanager.Getinstance().Getviewfromviewid(typeof(Detaildescriptionview).Name);

            Uistack.Getinstance().Openview(caseview, Viewstatus.HIDE);
            Uistack.Getinstance().Openview(detailview, Viewstatus.HIDE);
            Uistack.Getinstance().Openview(scanview, Viewstatus.HIDE);
        }


        public void Update()
        {
            if (SKassetbundlehelper.instance.bundlewww != null)
            {
                downloadprogress.fillAmount = SKassetbundlehelper.instance.bundlewww.progress;
                if (downloadprogress.fillAmount >= 0.95f)
                {
                    downloadtext.text = !SKassetbundlehelper.instance.Checkcache(item.Getitemurl(), item.version) ? "<color=#3266BAFF>点击下载</color>" : "<color=white>进入体验</color>";
                }
            }
        }


        /// <summary>
        /// 绑定按钮事件
        /// </summary>
        private void Btneventbind()
        {

            Scannermanager.Getinstance().onparsingitemend = Viewop;

            //获取识别图缩略图
            markless.onClick.AddListener(() =>
            {
                markerlessview.SetActive(true);
                Browser.Getinstance()
                    .GetMarkless(item.thumbnails, Uimanager.Getinstance().Setmarkerless, null, markerlessimage);
            });

            //关闭识别图浮窗
            markerlessclosebtn.onClick.AddListener(() =>
            {
                markerlessimage.sprite = null;
                Resources.UnloadUnusedAssets();
            });

            //下载识别图
            markerlesscdownloadbtn.onClick.AddListener(() =>
            {
                DownloadMarkless();
            });

            //下载资源
            download.onClick.AddListener(() =>
            {
                Downloadasset();
            });

            //播放视频
            videoplayerbtn.onClick.AddListener(() =>
            {
                videopayer.GetComponent<Button>().interactable = true;
                videoplayerbtn.gameObject.SetActive(false);
                if (tmpthumbnails != null)
                {
                    DestroyImmediate(tmpthumbnails);
                    videopayer.texture = null;
                }
                mediaplayer.Play();
            });

            //暂停视频
            videopayer.GetComponent<Button>().onClick.AddListener(() =>
            {
                videopayer.GetComponent<Button>().interactable = false;
                videoplayerbtn.gameObject.SetActive(true);
                mediaplayer.Pause();
            });




            favorite.onClick.AddListener(() =>
            {
                //TODO:write data to dbs.
                Adddatatofavoritedb();
            });
        }

        private void Adddatatofavoritedb()
        {
            Sqlitehelper helper = new Sqlitehelper("data source=skbrowser.db");


            SqliteDataReader sdr = helper.Readfromid("favorite_test", "id", item.id.ToString());
            if (sdr.HasRows)
            {
                Uimanager.Getinstance().Showtips("已取消收藏");
                helper.Delete("favorite_test", "id", item.id.ToString());
                helper.CloseSqlConnection();
                favorite.GetComponent<Image>().sprite = Resources.Load<Sprite>("Ui/btn_like_nor");
                Favoriteview favoriteview = FindObjectOfType<Favoriteview>();

                if (!favoriteview.favoritegameobjects.ContainsKey(item.id)) return;
                Destroy(favoriteview.favoritegameobjects[item.id]);
                favoriteview.favoritegameobjects.Remove(item.id);
                if (!favoriteview.favoriteoperation.favorite.ContainsKey(item.id)) return;
                favoriteview.favoriteoperation.favorite.Remove(item.id);
                return;
            }

            string itemjson = JsonUtility.ToJson(item);

            helper.InsertInto("favorite_test", new string[] { item.id.ToString(), item.itemname, itemjson });
            Uimanager.Getinstance().Showtips("已添加至收藏");
            favorite.GetComponent<Image>().sprite = Resources.Load<Sprite>("Ui/btn_like_sel");
        }

        /// <summary>
        /// 获取识别图图片的回调
        /// </summary>
        /// <param name="_bytes"></param>
        /// <param name="_image"></param>
        private void Setmarkerless(byte[] _bytes, object _image)
        {
            Texture2D t2d = new Texture2D(128, 128, TextureFormat.RGB24, false);
            t2d.LoadImage(_bytes);

            markerlessimage.sprite = Sprite.Create(t2d, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f), 100, 0, SpriteMeshType.FullRect);
        }

        /// <summary>
        /// 绑定到下载识别图按钮的按钮事件
        /// </summary>
        private void DownloadMarkless()
        {
            Browser.Getinstance().GetMarkless(item.trackerurl, GetMarkerless, null, markerlessimage);
        }

        /// <summary>
        /// 获取识别图图片回调
        /// </summary>
        /// <param name="_bytes"></param>
        /// <param name="_image"></param>
        private void GetMarkerless(byte[] _bytes, object _image)
        {
            //生成照片名称
            string filename = Fileoperation.Generatefilename(".jpg");
            string path = Scannermanager.Getinstance().projectconfig.photopath + "/" + filename;

            //存入本地相册
            File.WriteAllBytes(path, _bytes);
            Crossplatformbridge.Getinstance().Savephoto("/SKbrowser/" + filename);
            StartCoroutine(Uimanager.Getinstance().Waittoshowtips("识别图已存储到相册"));
        }

        private void Ondownloaded(object _object)
        {
            isdownloading = false;
        }
    }
}

