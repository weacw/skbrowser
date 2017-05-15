using System;
using System.Runtime.InteropServices;
using UnityEngine.UI;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Crossplatformbridge
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Crossplatformbridge : Singleton<Crossplatformbridge>
    {



        [DllImport("__Internal")]
        private static extern void _SaveVideoToAblum(string _url);
        public static void SaveVideoToAblum(string _url)
        {
            _SaveVideoToAblum(_url);
        }


        [DllImport("__Internal")]
        private static extern void _SavePhotoToAblum(string _url);
        public static void SavePhotoToAblum(string _url)
        {
            _SavePhotoToAblum(_url);
        }



        [DllImport("__Internal")]
        private static extern void _ReEncoding(string _inputurl,string _outputurl);
        public static void ReEncoding(string _inputurl, string _outputurl)
        {
            _ReEncoding(_inputurl,_outputurl);
        }




        public void Savevideo(string _filepath,string _output=null)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.WindowsEditor:
                    Debug.Log("Save video at: " + _filepath);
                    break;

                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.WindowsPlayer:
                    Debug.Log("Save video at: " + _filepath);
                    break;

                case RuntimePlatform.IPhonePlayer:
                    if (string.IsNullOrEmpty(_output)) return;
                    ReEncoding(_filepath, _output);
                    break;

                case RuntimePlatform.Android:
                   
                    break;
            }
        }





        /// <summary>
        /// 保存照片
        /// </summary>
        /// <param name="_filepath"></param>
        public void Savephoto(string _filepath)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.WindowsEditor:
                    Debug.Log("Save photo at: "+_filepath);
                    break;

                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.WindowsPlayer:
                    Debug.Log("Save photo at: " + _filepath);
                    break;

                case RuntimePlatform.IPhonePlayer:
                    SavePhotoToAblum(_filepath);                                 
                    break;

                case RuntimePlatform.Android:
                    Calljavabridge(_filepath, "Startsavephoto");
                    break;
            }
        }


        /// <summary>
        /// 调用安卓原生方法
        /// </summary>
        /// <param name="_filepath"></param>
        /// <param name="_func"></param>
        private void Calljavabridge(string _filepath, string _func)
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call(_func, _filepath);
        }

        /// <summary>
        /// 如上
        /// </summary>
        /// <param name="_func"></param>
        /// <returns></returns>
        private string Calljavabridge(string _func)
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            return jo.Call<string>(_func);
        }

        /// <summary>
        /// 获取相册路径
        /// </summary>
        /// <returns></returns>
        public string Getpath()
        {
            string path = null;
            switch (Application.platform)
            {
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.WindowsEditor:
                    path = Scannermanager.Getinstance().projectconfig.appenvironmentalpath;
                    break;

                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.WindowsPlayer:
                    path = Scannermanager.Getinstance().projectconfig.appenvironmentalpath;
                    break;

                case RuntimePlatform.IPhonePlayer:
                    path = Scannermanager.Getinstance().projectconfig.appenvironmentalpath;
                    break;

                case RuntimePlatform.Android:
                    path = Calljavabridge("GetDCIMpath");
                    break;

            }
            return path;
        }
    }
}
