using UnityEngine.UI;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Aboutview
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Aboutview : Baseview
    {
        public Button returnbtn;
        public override void Bindingeventstobtn()
        {
            returnbtn.onClick.AddListener(() =>
            {
                Uistack.Getinstance().Return();
            });
        }

        public override void Initview()
        {
            base.Initview();
            viewenabled = true;
            moveto = new Vector3(selfrecttransform.anchoredPosition.x, 0);
            orignalpos = new Vector3(0, 0);
        }
    }
}
