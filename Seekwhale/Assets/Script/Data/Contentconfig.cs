namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Contentconfig
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    [System.Serializable]
    public class Contentconfig : ScriptableObject
    {
        public Bundletype bundletype = Bundletype.MODEL;
        public Bundlelosetype bundlelosetype = Bundlelosetype.SCREEN2D;

        public string videourl;

        public Vector3 bundleposition = Vector3.zero;
        public Vector3 bundlerotation = Vector3.zero;
        public Vector3 bundlescale = Vector3.one;
        public Vector3 bundleposistionoffset = Vector3.zero;
        public bool gestureoperation = false;
    }
}
