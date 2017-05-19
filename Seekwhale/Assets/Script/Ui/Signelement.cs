using System;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Signelement
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Signelement : Baseview
    {        
        public override void Bindingeventstobtn()
        {
            
        }

        public override void Updateviewstatus(Viewstatus _viewstatus)
        {            
            switch (_viewstatus)
            {
                case Viewstatus.SHOW:
                    self.gameObject.SetActive(false);
                    break;
                case Viewstatus.HIDE:
                    self.gameObject.SetActive(true);
                    break;
            }
        }
    }
}
