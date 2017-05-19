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
                Uistack.Getinstance().Return(2);
            });
        }

        public override void Initview()
        {
            base.Initview();
            viewenabled = false;
            movementtoffset =Vector3.zero;
            originaloffset = self.anchoredPosition3D;
            detailsetup = GetComponent<Detailsetup>();
            detailsetup.enabled = false;
        }

        public override void Updateviewstatus(Viewstatus _viewstatus)
        {
            base.Updateviewstatus(_viewstatus);
            if (!viewenabled)
                detailsetup.enabled = viewenabled;
        }
    }
}
