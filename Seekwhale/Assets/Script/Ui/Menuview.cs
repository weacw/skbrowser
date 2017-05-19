namespace SeekWhale
{
    using DG.Tweening;
    using System;
    /*
* 功 能： N/A
* 类 名： Menuview
* Email: paris3@163.com
* 作 者： NSWell
* Copyright (c) SeekWhale. All rights reserved.
*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class Menuview : Baseview
    {
        public Text spaceOccupiedtext;

        public Button back;
        public Button favorite;
        public Button histroy;
        public Button cleancach;
        public Button aboutbtn;
        public override void Bindingeventstobtn()
        {
            back.onClick.AddListener(() =>
            {
                Uistack.Getinstance().Return(2);
            });
            cleancach.onClick.AddListener(() =>
            {
                Caching.CleanCache();
                Calculaterspaceoccupied();
            });
            aboutbtn.onClick.AddListener(() =>
            {
                Baseview show = Uimanager.Getinstance().Getviewfromviewid(typeof(Aboutview).Name);
                Uistack.Getinstance().Openview(show, Viewstatus.SHOW);
            });
        }

        public override void Updateviewstatus(Viewstatus _viewstatus)
        {
            Calculaterspaceoccupied();
            base.Updateviewstatus(_viewstatus);
        }

        private void Calculaterspaceoccupied()
        {
            spaceOccupiedtext.text = (Caching.spaceOccupied / Mathf.Pow(1024, 2)).ToString("0.0") + " M";
        }

        public override void Initview()
        {
            base.Initview();
            movementtoffset = Vector3.zero;
            originaloffset = self.anchoredPosition3D;
        }
    }
}
