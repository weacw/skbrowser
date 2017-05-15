namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Baserendererop
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System;
    using System.Collections.Generic;
    using UnityEngine;
    [System.Serializable]
    public class Baserendererop
    {
        public bool igrone;
        [HideInInspector]
        public string nodename;
        public Renderer renderer;
        public float duration = 1;
        [HideInInspector]
        public float smoothness;
        [HideInInspector]
        public float metallic;
        public Color maincolor = Color.white;
    }


    public class Dissectedaction
    {
        public Action Beforeanatomy;
        public Action Afteranatomy;
    }
}
