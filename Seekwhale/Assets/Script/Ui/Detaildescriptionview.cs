using UnityEngine.UI;

namespace SeekWhale
{
    using DG.Tweening;
    using System;
    /*
* 功 能： N/A
* 类 名： Detaildescriptionview
* Email: paris3@163.com
* 作 者： NSWell
* Copyright (c) SeekWhale. All rights reserved.
*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Detaildescriptionview : Baseview
    {
        public Button returnbtn;
        public Detailsetup detailsetup;
        public override void Bindingeventstobtn()
        {
            Baseview hide = Uimanager.Getinstance().Getviewfromviewid(typeof(Showcaseview).Name);

            returnbtn.onClick.AddListener(() =>
            {
                Uistack.Getinstance().Return();
            });
        }

        public override void Initview()
        {
            base.Initview();
            viewenabled = false;
            moveto = new Vector3(selfrecttransform.rect.width, 0, 0);
            orignalpos = new Vector3(0, 0);
            detailsetup = GetComponent<Detailsetup>();
            detailsetup.enabled = false;
        }

        public override void Updateviewstatus()
        {
            base.Updateviewstatus();
            if (!viewenabled)
                detailsetup.enabled = viewenabled;
        }
    }
}
