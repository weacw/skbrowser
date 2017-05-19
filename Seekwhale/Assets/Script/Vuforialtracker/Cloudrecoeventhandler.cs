namespace SeekWhale
{
    using System;
    /*
* 功 能： N/A
* 类 名： Cloudrecoeventhandler
* Email: paris3@163.com
* 作 者： NSWell
* Copyright (c) SeekWhale. All rights reserved.
*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Vuforia;
    public delegate void Ontargetfound(string _trackerid);
    public class Cloudrecoeventhandler : Singleton<Cloudrecoeventhandler>, ICloudRecoEventHandler
    {
        protected Scannermanager sm;
        private CloudRecoBehaviour cloudrecobehaviour;

        public ImageTargetBehaviour imagetargettemplate;
        //---------------------- Unity call back function----------------------------------
        //--------------------------初始化当前脚本变量------------------------------------
        private void Awake()
        {
            cloudrecobehaviour = GetComponent<CloudRecoBehaviour>();
            if (cloudrecobehaviour)
                cloudrecobehaviour.RegisterEventHandler(this);

            sm = Scannermanager.Getinstance();
            Stopscanning();
        }



        //---------------------------提供外部调用的方法----------------------------------------
        /// <summary>
        /// 重置扫描
        /// </summary>
        public void Restartscanning()
        {
            if (sm.metadaats != null)
            {
                sm.metadaats.trackerid = null;
                sm.metadaats = null;
            }
            if (cloudrecobehaviour)
            {
                cloudrecobehaviour.CloudRecoEnabled = true;
                sm.Getcurrecostatus = Scanstatus.GOTOSCAN;
            }
        }

        /// <summary>
        /// 停止扫描
        /// </summary>
        public void Stopscanning()
        {
            if (cloudrecobehaviour)
            {
                cloudrecobehaviour.CloudRecoEnabled = false;
                sm.Getcurrecostatus = Scanstatus.STOPSCANNING;
            }
        }




        //----------------------- Vuforia implemente function------------------------------
        public void OnInitialized()
        {
            Debug.Log("Cloud Reco initialized");
            sm.isinited = true;
            Uimanager.Getinstance().Showtips("初始化完成");            
        }

        public void OnInitError(TargetFinder.InitState _initError)
        {
            Debug.Log("Cloud Reco init error " + _initError.ToString());
        }

        public void OnUpdateError(TargetFinder.UpdateState _updateError)
        {
#if UNITY_EDITOR
            Debug.Log("Cloud Reco update error " + _updateError.ToString());
#endif
        }


        /// <summary>
        /// 识别到目标
        /// </summary>
        /// <param name="_targetSearchResult"></param>
        public void OnNewSearchResult(TargetFinder.TargetSearchResult _targetSearchResult)
        {
            if (!sm.isinited) return;
            //1.设置当前状态为得到扫描数据结果
            //2.关闭云识别功能模块
            //3.反序列化扫描得到的数据到metadata类型对象
            //4.使用反序列化得到的对象变量id对服务器数据库发起请求
            //5.激活当前识别追踪容器
            sm.Getcurrecostatus = Scanstatus.SCANTHERESULTS;
            cloudrecobehaviour.CloudRecoEnabled = false;

            //------------将json反序列化为metadata类型对象-------------------
            //sm.metadaats = JsonUtility.FromJson<Metadata>(_targetSearchResult.MetaData);

            //------------得到扫描结果对象后向服务器发起查询请求事件------------
            Browser.Getinstance().Getitems(_targetSearchResult.UniqueTargetId,
                                                        Scannermanager.Getinstance().Parsingitemjson,
                                                        Scannermanager.Getinstance().Onerror);

            //---------------------激活template追踪容器------------------------
            if (imagetargettemplate)
            {
                ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
                ImageTargetBehaviour imageTargetBehaviour =
                 (ImageTargetBehaviour)tracker.TargetFinder.EnableTracking(_targetSearchResult, imagetargettemplate.gameObject);
            }
        }

        /// <summary>
        /// 改变扫描模式的时候被调用
        /// </summary>
        /// <param name="_scanning"></param>
        public void OnStateChanged(bool _scanning)
        {
            sm.isscanning = _scanning;
            if (!_scanning)
            {
                return;
            }

            ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            tracker.TargetFinder.ClearTrackables(false);
        }
    }
}
