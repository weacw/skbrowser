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
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using DG.Tweening;
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Baseview : MonoBehaviour
    {
        [SerializeField]
        internal string viewid;
        [SerializeField]
        internal bool viewenabled;
        [SerializeField]
        internal bool isinited;
        internal TweenCallback callback;

        public AnimationCurve actioncurve;
        [Range(0.1f, 1f)]
        public float duration;
        public Vector3 moveto;

        protected RectTransform selfrecttransform;
        protected CanvasGroup canvasgroup;
        protected Vector3 orignalpos;
        [SerializeField]
        protected Getdatas getdatas;

        public virtual void Updateviewstatus()
        {
            Vector3 targetpos = Vector3.zero;
            float fadevalue = 0;


            if (viewenabled)
            {
                targetpos = orignalpos;
                fadevalue = 1;
            }
            else
            {
                //窗体正处于显示状态，触发后回收窗体 selfrecttransform.anchoredPosition.x, -selfrecttransform.rect.height
                targetpos = moveto;
                fadevalue = 0;
            }



            Tweener domove = null;
            Tweener dofade = null;


            domove = selfrecttransform.DOAnchorPos(targetpos, duration);
            dofade = canvasgroup.DOFade(fadevalue, duration + 0.25f);
            domove.SetEase(actioncurve);
            domove.SetAutoKill(true);
            dofade.SetAutoKill(true);
            if (callback != null)
            {
                domove.OnComplete(() =>
                {
                    callback.Invoke();
                });
            }
            dofade.OnComplete(() =>
             {
                 viewenabled = !viewenabled;
             });
        }
        public abstract void Bindingeventstobtn();
        public virtual void Initview()
        {
            canvasgroup = GetComponent<CanvasGroup>();
            selfrecttransform = GetComponent<RectTransform>();
            viewid = GetType().Name;
            viewenabled = false;
            Uimanager.Getinstance().Addviewtodictioary(viewid, this);
            Bindingeventstobtn();
            isinited = true;
        }

        protected virtual void Start()
        {
            if (!isinited)
                Initview();
        }
    }
}
