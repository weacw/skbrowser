using System;
using DG.Tweening;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Views
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Views : MonoBehaviour
    {
        public Action callback;
        public string Viewid { get; private set; }

        public bool viewvisible = false;

        [Header("View setup")]
        public AnimationCurve viewanimation;
        public float duration = 0.5f;

        [Header("On complete")]
        public float delayeforcallback;
        public bool callbackonlyondisplay = true;
        

        [Header("View posistion")]
        [SerializeField]protected Vector3 originaloffset;
        [SerializeField]protected Vector3 movementtoffset;


        protected RectTransform self;
        protected Tweener tweeners = null;

        /// <summary>
        /// 初始化view的设置
        /// </summary>
        public virtual void Viewinit()
        {
            self = GetComponent<RectTransform>();
            originaloffset = self.anchoredPosition3D;
            Viewid = GetType().Name;
        }


        /// <summary>
        /// 更新view的状态
        /// </summary>
        /// <param name="_viewstatus"></param>
        public virtual void Viewupdate(Viewstatus _viewstatus)
        {
            switch (_viewstatus)
            {
                case Viewstatus.HIDE:
                    tweeners = self.DOAnchorPos(originaloffset, duration);
                    tweeners.SetAutoKill(true);
                    break;
                case Viewstatus.SHOW:
                    tweeners = self.DOAnchorPos(movementtoffset, duration);
                    tweeners.SetAutoKill(true);
                    break;
            }

            if (_viewstatus == Viewstatus.HIDE && callbackonlyondisplay) return;
            if (callback != null && tweeners != null)
                tweeners.OnComplete(Executecallback);
        }


        /// <summary>
        /// 执行回调函数
        /// </summary>
        private void Executecallback()
        {
            StartCoroutine(Delayexecution());
        }

        /// <summary>
        /// 延迟回调函数调用
        /// </summary>
        /// <returns></returns>
        private IEnumerator Delayexecution()
        {
            yield return new WaitForSeconds(delayeforcallback);
            callback.Invoke();
        }
    }
}
