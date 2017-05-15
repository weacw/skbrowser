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
        protected override void Start()
        {
            base.Start();
        }

        public override void Initview()
        {
            base.Initview();
            getdatas = Getdatas.GETTING;
            viewenabled = false;
            moveto = new Vector3(selfrecttransform.anchoredPosition.x, -selfrecttransform.rect.height);
            orignalpos = new Vector3(selfrecttransform.anchoredPosition.x, 0);
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

        public override void Updateviewstatus()
        {
            if (callback == null)
                callback = Getdata;

            base.Updateviewstatus();
        }

        private void Getdata()
        {
            StartCoroutine(Waittingtoload());
        }

        private IEnumerator Waittingtoload()
        {
            yield return new WaitForEndOfFrame();       

            if (!viewenabled || getdatas != Getdatas.GETTING) yield break;
            if (Browser.Getinstance().GetNetreachable(false))
                Uimanager.Getinstance().Showtips("数据加载中...");
            //TODO:1.显示提示            
            Browser.Getinstance().Getcatergory(Uimanager.Getinstance().Getcategory, Uimanager.Getinstance().Showtips);
            getdatas = Getdatas.GETED;
        }
    }
}
