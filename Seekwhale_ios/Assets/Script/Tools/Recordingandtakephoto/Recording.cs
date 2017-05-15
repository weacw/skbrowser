using System;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Recording
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Recording
    {
        private bool disablesinglecoredevices = true;
        private int maxrecordingminuteslength = 15;
        private bool optimizeforlowmemory = true;
        private bool isreadyforreacording = false;
        private bool issupported = false;
        private bool isstarted = false;

        protected bool isinited;

        public Action<float> onrecording;
        public Action onrecorded;

        private float curtime;

        public bool Isstarted
        {
            get { return isstarted; }
        }

        /// <summary>
        /// 初始化操作以及插件参数回调等
        /// </summary>
        public void Initop()
        {
            Everyplay.SetDisableSingleCoreDevices(disablesinglecoredevices);
            Everyplay.SetMaxRecordingMinutesLength(maxrecordingminuteslength);
            Everyplay.SetLowMemoryDevice(optimizeforlowmemory);
            issupported = Everyplay.IsSupported();

            Everyplay.ReadyForRecording += OnReadyForRecording;

            Everyplay.RecordingStarted += Onstartrecording;
            Everyplay.RecordingStopped += Onstoprecording;

            isinited = true;
        }

        /// <summary>
        /// 已经准备完毕 可以开始录制
        /// </summary>
        /// <param name="enabled"></param>
        public void OnReadyForRecording(bool enabled)
        {
            if (enabled)
            {
                isreadyforreacording = enabled;
            }
        }

        /// <summary>
        /// 用于录屏操作开始的触发器 
        /// </summary>
        /// <param name="isrecording"></param>
        public void Dooperation(bool isrecording)
        {
            if (!isinited)
                Initop();
            isstarted = Everyplay.IsRecording();
            if (!issupported && !isrecording)
            {
                Uimanager.Getinstance().Showtips("当前硬件设备不支持该功能");
                return;
            }

            if (isrecording)
            {
                Debug.Log("停止录制" + isstarted);

                Everyplay.StopRecording();
            }
            else
            {
                Debug.Log("开始录制" + isstarted);

                Everyplay.StartRecording();
            }

        }

        /// <summary>
        /// 开始录制视频回调
        /// </summary>
        private void Onstartrecording()
        {
            isstarted = Everyplay.IsRecording();
            Debug.Log("开始录制" + isstarted);
            Uimanager.Getinstance().Showtips("开始录制");
        }

        /// <summary>
        /// 录屏结束后回调
        /// </summary>
        private void Onstoprecording()
        {
            isstarted = Everyplay.IsRecording();

            //视屏录制完毕后回调，一般用于隐藏恢复用户提示界面（UI）
            if (onrecorded != null)
                onrecorded();
            curtime = 0;

            //由于everplayer录屏插件录制的视频将存储到temporarycachpath中
            //故我们使用这个路径用C#代码来查找
            string tmpfilepath = Application.temporaryCachePath;

            //使用Directoryinfo 来搜索查询录制下来的视频
            DirectoryInfo dir = new DirectoryInfo(tmpfilepath);
            var files = dir.GetFiles("*.mp4", SearchOption.AllDirectories);
            var file = files.OrderByDescending(f => f.CreationTime).FirstOrDefault();
            if (file != null && string.IsNullOrEmpty(file.FullName)) return;

#if UNITY_ANDROID
            //搜索到视频后将其另存到我们指定的目录下            
            Fileoperation.Writefiletodisk(file.FullName, Scannermanager.Getinstance().projectconfig.recordingpath, Fileoperation.Generatefilename(".mp4"));
#endif



#if Unity_IPHONE
            //IOS中存储路径与安卓有差异
            tmpfilepath = tmpfilepath.Replace("Library/Caches", "tmp");
            string originpath = file.FullName;
            string temp = originpath.Replace(file.Name, file.Name + "_reencode.mp4");
            Crossplatformbridge.Getinstance().Savevideo(originpath, temp);
#endif

            Uimanager.Getinstance().Showtips("录像已存到本地");
        }

        /// <summary>
        /// 计时器，录像时长达到maxrecordingminuteslength时自动停止录像并保存到本地
        /// </summary>
        private void Calculaterecordingtime()
        {
            if (curtime >= maxrecordingminuteslength)
            {
                curtime = maxrecordingminuteslength;
                isstarted = false;
                Everyplay.StopRecording();
                return;
            }
            curtime += Time.deltaTime;

            //正处于录像回调回调，一般用户刷新用户界面的进度（例如当前录制视频的时长）
            if (onrecording != null)
                onrecording.Invoke(curtime / maxrecordingminuteslength);
        }

        /// <summary>
        /// 当开始录像时倒计时15s
        /// </summary>
        public void Update()
        {
            if (isstarted)
                Calculaterecordingtime();
        }
    }
}
