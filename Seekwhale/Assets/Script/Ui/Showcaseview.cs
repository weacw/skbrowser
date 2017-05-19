using System.Collections;

namespace SeekWhale
{
    using System;
    /*
* 功 能： N/A
* 类 名： Showcaseview
* Email: paris3@163.com
* 作 者： NSWell
* Copyright (c) SeekWhale. All rights reserved.
*/

    using UnityEngine;
    using UnityEngine.UI;

    public class Showcaseview : Baseview
    {
        public Button returnbtn;

        public GameObject verticalprefab;
        public GameObject horizontalprefab;

        public Transform verticalroot;
        public Transform horizontalroot;

        public override void Start()
        {
            base.Start();
        }

        public override void Initview()
        {
            base.Initview();
            getdatas = Getdatas.GETTING;
            movementtoffset =Vector3.zero;
            originaloffset = self.anchoredPosition3D;
            callback = Getdata;
        }

        public override void Bindingeventstobtn()
        {
            //show case view 返回至首页
            returnbtn.onClick.AddListener(() =>
            {
                Uistack.Getinstance().Return(2);
                getdatas = Getdatas.GETTING;
            });
        }

        public override void Updateviewstatus(Viewstatus _viewstatus)
        {
            base.Updateviewstatus(_viewstatus);
        }

        private void Getdata()
        {
            if (getdatas != Getdatas.GETTING) return;
            if (Browser.Getinstance().GetNetreachable(false))
                Uimanager.Getinstance().Showtips("数据加载中...");
            //TODO:1.显示提示            
            Browser.Getinstance().Getcatergory(Uimanager.Getinstance().Getcategory, Uimanager.Getinstance().Showtips);
            getdatas = Getdatas.GETED;
        }
    }
}