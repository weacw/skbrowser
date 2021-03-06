﻿using System;
using UnityEngine.UI;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Itemsetup
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Itemsetup : MonoBehaviour
    {
        public Image background;
        public Text title;
        public string unitid;
        public Button clickarea;

        public Item item;

        private Baseview detaildescriptionview;        

        /// <summary>
        /// 添加按钮事件-下载资源/呈现资源
        /// </summary>
        public void Addfuntobtn()
        {
           
            Uistack.Getinstance().Openview(detaildescriptionview, Viewstatus.SHOW);
            Setupdetails();
        }

        /// <summary>
        /// 设置detail panle 显示数据
        /// </summary>
        private void Setupdetails()
        {
            Detaildescriptionview ddv = ((Detaildescriptionview)detaildescriptionview);
            ddv.detailsetup.item = item;
            ddv.detailsetup.enabled = true;
        }

        private void Start()
        {
            detaildescriptionview = Uimanager.Getinstance().Getviewfromviewid(typeof(Detaildescriptionview).Name);


            clickarea.onClick.AddListener(Addfuntobtn);
#if VERSION2_0
            Debug.Log(item.thumbnails);
            Browser.Getinstance().GetMarkless(item.thumbnails, Uimanager.Getinstance().Setmarkerless, null, background);
#endif
        }
    }
}
