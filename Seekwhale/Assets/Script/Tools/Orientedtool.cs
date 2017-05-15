namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Orientedtool
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Orientedtool : MonoBehaviour
    {

        private Transform target;
        private Transform self;
        public bool updateoriented;
        public Vector3 orignaleuler;
        public Lockaxis lockaxis;
        Scannermanager scannermanager;
        [System.Serializable]
        public struct Lockaxis
        {
            public bool x;
            public bool y;
            public bool z;
        }

        private void Start()
        {
            self = this.transform;
            target = Camera.main.transform;
            scannermanager = Scannermanager.Getinstance();
        }


        public void Update()
        {
            switch (scannermanager.curtrackerstatus)
            {
                case Trackerstatus.LOSE:
                    updateoriented = false;
                    self.LookAt(target);
                    self.localEulerAngles = new Vector3(self.localEulerAngles.x, self.localEulerAngles.y, 0);
                    break;
                case Trackerstatus.FOUND:
                    updateoriented = true;
                    break;
            }

            if (!updateoriented) return;
            self.LookAt(target);
            Vector3 quat = self.localEulerAngles;
           
            //------------LOCKING--------------            
            if (lockaxis.x)
                quat.x = orignaleuler.x;
            if (lockaxis.y)
                quat.y = orignaleuler.y;
            if (lockaxis.z)
                quat.z = orignaleuler.z;
            //----------------------------------       

            self.localEulerAngles = quat;
        }
    }
}
