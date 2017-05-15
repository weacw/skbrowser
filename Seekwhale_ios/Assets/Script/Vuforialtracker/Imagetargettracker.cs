namespace SeekWhale
{
    using System;
    /*
* 功 能： N/A
* 类 名： Imagetargettraker
* Email: paris3@163.com
* 作 者： NSWell
* Copyright (c) SeekWhale. All rights reserved.
*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Vuforia;
    public delegate void Ontrackerhandler(Imagetargettracker _imagetargettracker, GameObject _imagetarget);
    public class Imagetargettracker : MonoBehaviour, ITrackableEventHandler
    {
        public Imagetargetdata targetdata;

        private TrackableBehaviour trackablebehaviour;

        private void Init()
        {
            trackablebehaviour = GetComponent<TrackableBehaviour>();
            targetdata = GetComponent<Imagetargetdata>();
        }

        private void OnEnable()
        {
            Init();
            Registerthistracker();
        }

        private void OnDisable()
        {
            Unregisterthistracker();
        }

        public void OnTrackableStateChanged(TrackableBehaviour.Status _previousstatus, TrackableBehaviour.Status _newstatus)
        {
            switch (_newstatus)
            {
                case TrackableBehaviour.Status.NOT_FOUND:
                case TrackableBehaviour.Status.UNKNOWN:
                case TrackableBehaviour.Status.UNDEFINED:
                    Scannermanager.Getinstance().Ontrackerloseevent(this, this.gameObject);
                    Debug.Log("Lose " + trackablebehaviour.TrackableName);
                    break;
                case TrackableBehaviour.Status.DETECTED:
                case TrackableBehaviour.Status.TRACKED:
                case TrackableBehaviour.Status.EXTENDED_TRACKED:
                    Scannermanager.Getinstance().Ontrackerfoundevent(this, this.gameObject);
                    Debug.Log("Found " + trackablebehaviour.TrackableName);
                    break;
            }
        }


        /// <summary>
        /// 订阅当前追踪器
        /// </summary>
        public void Registerthistracker()
        {

            if (!trackablebehaviour)
            {
                Debug.LogError(Globallogmsg.TRAKABLEEMPTY);
                return;
            }
            trackablebehaviour.RegisterTrackableEventHandler(this);
            Globallogmsg.Getregistermsg(Globallogmsg.Registertype.REGISTER, trackablebehaviour.TrackableName);
        }

        /// <summary>
        /// 取消订阅当前追踪器
        /// </summary>
        public void Unregisterthistracker()
        {
            if (!trackablebehaviour)
            {
                Debug.LogError(Globallogmsg.TRAKABLEEMPTY);
                return;
            }
            trackablebehaviour.UnregisterTrackableEventHandler(this);
            Globallogmsg.Getregistermsg(Globallogmsg.Registertype.UNREGISTER, trackablebehaviour.TrackableName);
        }
    }
}
