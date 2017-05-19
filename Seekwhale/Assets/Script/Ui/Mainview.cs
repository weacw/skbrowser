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
            Baseview hide = Uimanager.Getinstance().Getviewfromviewid(typeof(Mainview).Name);
            Debug.Log("ADD");
            showcase.onClick.AddListener(() =>
            {
                Baseview show = Uimanager.Getinstance().Getviewfromviewid(typeof(Showcaseview).Name);
                Uistack.Getinstance().Openview(show, Viewstatus.SHOW);
                Uistack.Getinstance().Openview(hide, Viewstatus.SHOW);
            });

            menu.onClick.AddListener(() =>
            {
                Baseview show = Uimanager.Getinstance().Getviewfromviewid(typeof(Menuview).Name);
                Uistack.Getinstance().Openview(show, Viewstatus.SHOW);
                Uistack.Getinstance().Openview(hide, Viewstatus.SHOW);
            });

            gotoscan.onClick.AddListener(() =>
            {
                Cloudrecoeventhandler.Getinstance().Restartscanning();

                Baseview show = Uimanager.Getinstance().Getviewfromviewid(typeof(Scanview).Name);
                Uistack.Getinstance().Openview(show, Viewstatus.SHOW);
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
