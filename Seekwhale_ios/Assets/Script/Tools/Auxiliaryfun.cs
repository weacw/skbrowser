using System;
using Vuforia;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Auxiliaryfun
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Auxiliaryfun : Singleton<Auxiliaryfun>
    {
        public Action<CameraDevice.CameraDirection> oncameraswitched;

        private bool flashison;
        public void Turnontheflash(bool _flashstatus)
        {
            if (!flashison)
                flashison = _flashstatus;
            else
                flashison = false;
            CameraDevice.Instance.SetFlashTorchMode(flashison);
        }

        public void Focuson()
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }

        public void Switchthecamera()
        {
            CameraDevice.CameraDirection camdirection = CameraDevice.Instance.GetCameraDirection();
            switch (camdirection)
            {
                case CameraDevice.CameraDirection.CAMERA_DEFAULT:
                case CameraDevice.CameraDirection.CAMERA_BACK:
                    CameraDevice.Instance.Stop();
                    CameraDevice.Instance.Deinit();

                    CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_FRONT);
                    CameraDevice.Instance.Start();
                    break;
                case CameraDevice.CameraDirection.CAMERA_FRONT:
                    CameraDevice.Instance.Stop();
                    CameraDevice.Instance.Deinit();

                    CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_DEFAULT);
                    CameraDevice.Instance.Start();
                    break;
            }
            if (oncameraswitched != null)
                oncameraswitched.Invoke(camdirection);
        }
    }
}
