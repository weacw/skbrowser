using System;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Takephoto
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Takephoto :  Ioperation
    {
        private int screenwidth, screenheight;

        internal Action doingbefore = null;
        internal Action doingafter = null;
        protected bool isinited;


        public void Initop()
        {
            isinited = true;
            screenheight = Screen.height;
            screenwidth = Screen.width;
        }

        public void Dooperation()
        {
            if(!isinited)Initop();
            Takephotoorrecord.Getinstance().StartCoroutine(TakephotoCoroutine());
        }

        private IEnumerator TakephotoCoroutine()
        {
            if (doingbefore != null) doingbefore.Invoke();

            yield return new WaitForEndOfFrame();

            //获取屏幕上的所有像素写到texture2d内并保存
            Texture2D t2d = new Texture2D(screenwidth, screenheight, TextureFormat.ARGB32, false);
            t2d.ReadPixels(new Rect(0, 0, screenwidth, screenheight), 0, 0, false);
            t2d.Apply(false);
            string filename = Fileoperation.Generatefilename(".jpg");
            byte[] bytes = t2d.EncodeToJPG();
            Fileoperation.Writefiletodisk(Scannermanager.Getinstance().projectconfig.photopath, filename, bytes);
            Crossplatformbridge.Getinstance().Savephoto(Scannermanager.Getinstance().projectconfig.photopath+"/"+filename);
            if (doingafter != null) doingafter.Invoke();
        }
    }
}
