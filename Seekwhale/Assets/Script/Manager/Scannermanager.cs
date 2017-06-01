using System;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Scannermanager
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Vuforia;

    public class Scannermanager : Singleton<Scannermanager>
    {
        public Projectconfig projectconfig;
        public Baseoperation baseoperation;
        public Trackerstatus curtrackerstatus;


        public Scanstatus Getcurrecostatus;// { get;  set; }
        public Metadata metadaats { get; set; }
        public bool isscanning { get; set; }
        public Action onparsingitemend;

        internal bool isinited;

        protected Imagetargettracker imagetracker;

        public void Awake()
        {
            if(projectconfig!=null)
            projectconfig.Setup();            
        }

        public void Onerror(string _errormsg)
        {
            Uimanager.Getinstance().Showtips(_errormsg);
            Getcurrecostatus = Scanstatus.GOTOSCAN;
            Uimanager.Getinstance().Hideprogress();
        }

        public void Parsingitemjson(string _msg)
        {
            //--------------------------反序列化至items类型--------------------
            Item item = Browser.Getinstance().Getdatafromjson<Item>(_msg);
            //--------------------------资源加载-------------------------------
            StartCoroutine(SKassetbundlehelper.instance.Paringbundle(item.Getitemurl(), item.version, Parsingbundlecallback));
            Getcurrecostatus = Scanstatus.SCANTHERESULTS;
            //TODO:显示下载UI进度条
            Uimanager.Getinstance().Showprogress();
        }

        internal void Parsingbundlecallback(Object obj)
        {
            if (this.imagetracker == null)
            {
                imagetracker = FindObjectOfType<Imagetargettracker>();
            }

            Getcurrecostatus = Scanstatus.SCANISDONE;
            //TODO:隐藏下载UI进度条
            Uimanager.Getinstance().Hideprogress();

            GameObject tmp = GameObject.Instantiate((GameObject)obj);
            tmp.transform.SetParent(Cloudrecoeventhandler.Getinstance().imagetargettemplate.transform);
            tmp.transform.localPosition = Vector3.zero;
            tmp.transform.localEulerAngles = Vector3.zero;

            imagetracker.targetdata.losetype = baseoperation.config.bundlelosetype;
            imagetracker.targetdata.rotation = baseoperation.config.bundlerotation;
            imagetracker.targetdata.scale = baseoperation.config.bundlescale;
            imagetracker.targetdata.centeroffset = baseoperation.config.bundleposistionoffset;
            imagetracker.targetdata.Instantiatedobject = tmp;


            switch (curtrackerstatus)
            {
                case Trackerstatus.LOSE:
                    Ontrackerloseevent(imagetracker, null);
                    break;
                case Trackerstatus.FOUND:
                    Ontrackerfoundevent(imagetracker, null);
                    break;
            }


            if(onparsingitemend!=null)
                onparsingitemend.Invoke();
        }


        //-------------------------------------Tracker call back------------------------------------------
        public void Ontrackerfoundevent(Imagetargettracker _imagetargettracker, GameObject _imagetarget)
        {
            //TODO:版本蓝图
            //1.识别图识别到后操作分为三大类型：A.基础类型操作（base),B.基于基础类型之上添加自定义操作(base+Custom),C.完全自定义操作(freedom)
            //2.识别到识别图后读取设置进行对识别图识别后进行区分操作
            //3.识别到识别图后进行的操作写入内容资源内部
            //4.调用资源内部方法进行执行

            if (!Browser.Getinstance().GetNetreachable(false)) return;


            if (imagetracker != null)
            {
                if (!imagetracker.Equals(_imagetargettracker))
                    imagetracker = _imagetargettracker;
            }
            else
            {
                imagetracker = _imagetargettracker;
            }

            curtrackerstatus = Trackerstatus.FOUND;
            Scanview scanview = (Scanview)Uimanager.Getinstance().Getviewfromviewid(typeof(Scanview).Name);
            scanview.Updateviewstatus(Viewstatus.SHOW);
            if (_imagetargettracker.targetdata.Instantiatedobject != null)
                _imagetargettracker.targetdata.Brokenlink();
        }

        public void Ontrackerloseevent(Imagetargettracker _imagetargettracker, GameObject _imagetarget)
        {
            curtrackerstatus = Trackerstatus.LOSE;

            if (_imagetargettracker.targetdata == null) return;
            switch (_imagetargettracker.targetdata.losetype)
            {
                case Bundlelosetype.DESTROY:
                    _imagetargettracker.targetdata.Releaseinstantiatedtarget();
                    break;
                case Bundlelosetype.HIDING:
                    _imagetargettracker.targetdata.Hideinstantiatedobject();
                    break;
                case Bundlelosetype.SCREEN2D:
                    _imagetargettracker.targetdata.Updatetocenter();
                    break;
            }
        }
    }
}
