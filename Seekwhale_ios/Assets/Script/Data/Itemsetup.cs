using System;
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

        /// <summary>
        /// 添加按钮事件-下载资源/呈现资源
        /// </summary>
        public void Addfuntobtn()
        {
            Baseview show = Uimanager.Getinstance().Getviewfromviewid(typeof(Detaildescriptionview).Name);
            Uistack.Getinstance().Douiop(show,Uistackoptype.SHOW,show.viewid);
            Setupdetails();
        }

        /// <summary>
        /// 设置detail panle 显示数据
        /// </summary>
        private void Setupdetails()
        {
            Baseview show = Uimanager.Getinstance().Getviewfromviewid(typeof(Detaildescriptionview).Name);
            Detaildescriptionview ddv=((Detaildescriptionview) show);
            ddv.detailsetup.item = item;
            ddv.detailsetup.enabled = true;
        }

        private void Start()
        {
            clickarea.onClick.AddListener(Addfuntobtn);
        }
    }
}
