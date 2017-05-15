namespace SeekWhale
{
    using System;
    /*
* 功 能： N/A
* 类 名： Physicalinput
* Email: paris3@163.com
* 作 者： NSWell
* Copyright (c) SeekWhale. All rights reserved.
*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Physicsinput:Ibaseinput
    {
        private bool isinited;
        private Physicsinputmoduloption option;


        public  void Whenuserclicktoretuanresult(out GameObject _result)
        {
            _result = null;
            if (!isinited)
            {
                Debug.LogError("Error: Physicalinput is not inited!");
                return;
            }
            option.mray = option.maincam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(option.mray, out option.raycasthit, 10000, option.layermask)) return;
            _result = option.raycasthit.collider.gameObject;
        }

        public void Onrelease()
        {
            option.canop = false;
            option.maincam = null;
            option.result = null;
            option = null;
            //--------------------
            isinited = false;
        }

        public void Modulinit(Userinputoption _option)
        {
            option = (Physicsinputmoduloption)_option;
            isinited = true;
        }
    }
    [System.Serializable]
    public class Physicsinputmoduloption : Userinputoption
    {
        public Ray mray;
        public RaycastHit raycasthit;
        public Camera maincam;
        public LayerMask layermask;
    }
}
