namespace SeekWhale
{
    using DG.Tweening;
    using System;
    /*
* 功 能： N/A
* 类 名： Mainview
* Email: paris3@163.com
* 作 者： NSWell
* Copyright (c) SeekWhale. All rights reserved.
*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class Mainview : Baseview
    {
        public Button showcase;
        public Button menu;
        public Button gotoscan;
        public override void Initview()
        {
            base.Initview();
            movementtoffset = new Vector3(self.anchoredPosition.x, -self.rect.height);
            originaloffset = self.anchoredPosition3D;
        }

        public override void Bindingeventstobtn()
        {

            //需要隐藏的界面
            Baseview hide = Uimanager.Getinstance().Getviewfromviewid(typeof(Mainview).Name);


            //打开showcase view
            Baseview showcaseview = Uimanager.Getinstance().Getviewfromviewid(typeof(Showcaseview).Name);
            showcase.onClick.AddListener(() =>
            {
                
                Uistack.Getinstance().Openview(showcaseview, Viewstatus.SHOW);
                Uistack.Getinstance().Openview(hide, Viewstatus.SHOW);
            });


            //打开menu view
            Baseview menuview = Uimanager.Getinstance().Getviewfromviewid(typeof(Menuview).Name);
            menu.onClick.AddListener(() =>
            {
                
                Uistack.Getinstance().Openview(menuview, Viewstatus.SHOW);
                Uistack.Getinstance().Openview(hide, Viewstatus.SHOW);
            });


            //打开scan view
            Baseview scanview = Uimanager.Getinstance().Getviewfromviewid(typeof(Scanview).Name);
            gotoscan.onClick.AddListener(() =>
            {
                Cloudrecoeventhandler.Getinstance().Restartscanning();
                Uistack.Getinstance().Openview(scanview, Viewstatus.SHOW);
                Uistack.Getinstance().Openview(hide, Viewstatus.SHOW);
            });
        }

        public override void Updateviewstatus(Viewstatus _viewstatus)
        {                      
            base.Updateviewstatus(_viewstatus);
            Uimanager.Getinstance().Getviewfromviewid(typeof (Signelement).Name).Updateviewstatus(_viewstatus);
        }
    }
}
