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
            viewenabled = true;
            moveto = new Vector3(selfrecttransform.anchoredPosition.x, -selfrecttransform.rect.height);
            orignalpos = new Vector3(selfrecttransform.anchoredPosition.x, 0);
        }

        public override void Bindingeventstobtn()
        {
            Baseview hide = Uimanager.Getinstance().Getviewfromviewid(typeof(Mainview).Name);

            showcase.onClick.AddListener(() =>
            {
                Baseview show = Uimanager.Getinstance().Getviewfromviewid(typeof(Showcaseview).Name);
                Uistack.Getinstance().Douiop(hide, Uistackoptype.HIDE,hide.viewid);
                Uistack.Getinstance().Douiop(show, Uistackoptype.SHOW,show.viewid);
            });

            menu.onClick.AddListener(() =>
            {
                Baseview show = Uimanager.Getinstance().Getviewfromviewid(typeof(Menuview).Name);
                Uistack.Getinstance().Douiop(hide, Uistackoptype.HIDE,hide.viewid);
                Uistack.Getinstance().Douiop(show, Uistackoptype.SHOW,show.viewid);
            });

            gotoscan.onClick.AddListener(() =>
            {
                Cloudrecoeventhandler.Getinstance().Restartscanning();

                Baseview show = Uimanager.Getinstance().Getviewfromviewid(typeof(Scanview).Name);
                Uistack.Getinstance().Douiop(hide, Uistackoptype.HIDE,hide.viewid);
                Uistack.Getinstance().Douiop(show, Uistackoptype.SHOW,show.viewid);
            });
        }

        public override void Updateviewstatus()
        {
            base.Updateviewstatus();
        }
    }
}
