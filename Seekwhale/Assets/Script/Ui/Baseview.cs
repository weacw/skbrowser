using System;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Baseview
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using UnityEngine;
    using DG.Tweening;
    public abstract class Baseview : MonoBehaviour
    {


        public Action ontweenover;
        [SerializeField]
        internal string viewid;
        [SerializeField]
        internal bool viewenabled;
        [SerializeField]
        internal bool isinited;
        [SerializeField]
        internal bool callbackonlydisplay = true;
        [SerializeField]
        internal float delayeforcallback = 1f;


        internal TweenCallback callback;

        public AnimationCurve actioncurve;
        [Range(0.1f, 1f)]
        public float duration;

        protected RectTransform self;


        [SerializeField]
        protected Vector3 movementtoffset;
        [SerializeField]
        protected Getdatas getdatas;

        protected Vector3 originaloffset;


        public virtual void Updateviewstatus(Viewstatus _viewstatus)
        {
            Tweener tweeners = null;
            switch (_viewstatus)
            {
                case Viewstatus.HIDE:
                    if (Vector2.Distance(originaloffset, self.anchoredPosition) <= 1)
                        tweeners = self.DOAnchorPos(movementtoffset, duration);
                    else
                        tweeners = self.DOAnchorPos(originaloffset, duration);
                    tweeners.SetAutoKill(true);
                    break;
                case Viewstatus.SHOW:
                    tweeners = self.DOAnchorPos(movementtoffset, duration);
                    tweeners.SetAutoKill(true);


                    if (callback != null && tweeners != null)
                        tweeners.OnComplete(() =>
                        {
                            Executecallback();
                        });

                    break;
            }
         //   tweeners.SetEase(actioncurve);
            if (ontweenover != null)
                tweeners.OnComplete(ontweenover.Invoke);
        }

        public abstract void Bindingeventstobtn();

        public virtual void Initview()
        {

            self = GetComponent<RectTransform>();
            originaloffset = self.anchoredPosition3D;
            viewid = GetType().Name;

            Uimanager.Getinstance().Addviewtodictioary(viewid, this);
            Bindingeventstobtn();
        }

        public virtual void Start()
        {
            Initview();
        }




        private void Executecallback()
        {
            StartCoroutine(Delayexecution());
        }


        private IEnumerator Delayexecution()
        {
            yield return new WaitForSeconds(delayeforcallback);
            callback.Invoke();
        }
    }
}
