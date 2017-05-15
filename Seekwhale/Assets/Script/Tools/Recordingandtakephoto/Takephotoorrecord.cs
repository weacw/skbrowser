using System;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Takephotoorrecord
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Takephotoorrecord : Singleton<Takephotoorrecord>
    {
        public Recording Recording;
        public Takephoto Takephoto { get; private set; }

        private float lasttime;
        private bool isinvoked;
        private bool ispress;
        private bool isdown;
        public void Dooperationdown()
        {
            if (isdown) return;
            lasttime = Time.time;
            isdown = true;
        }

        public void Dooperationup()
        {
            isdown = false;
            if (!ispress)
            {
                Debug.Log("Takephoto");
                Takephoto.Dooperation();
            }
            else
            {
                Debug.Log("Record");
                ispress = false;
                Recording.Dooperation(true);
            }
        }
        private void Awake()
        {
            Takephoto = new Takephoto();
        }

        private void Update()
        {
            if (!isdown || ispress) return;
            if (Time.time - lasttime > 0.25f)
            {
                ispress = true;
                Recording.Dooperation(false);
                lasttime = Time.time;
            }
        }
    }
}
