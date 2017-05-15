namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Item
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    [System.Serializable]
    public class Item
    {
        public int id;
        public string trackerid;
        public bool visiblc;
        public int version;
        public string itemname;
        public string category;
        public Bundletype bunletype;
        public string description;
        public string thumbnails;
        public string tutourl;
        public string tutorthumbnail;
        public string bundleurlandroid;
        public string bundleurlios;
        public string trackerurl;



        public string Getitemurl()
        {
            string tmp = null;
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    tmp = bundleurlandroid;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    tmp = bundleurlios;
                    break;
#if UNITY_EDITOR
                case RuntimePlatform.WindowsEditor:
                    tmp = bundleurlandroid;
                    break;
                case RuntimePlatform.OSXEditor:
                    tmp = bundleurlios;
                    break;
#endif
            }
            return tmp;
        }
    }

    [System.Serializable]
    public class Itemlist
    {
        public List<Item>  result = new List<Item>();
    }
}
