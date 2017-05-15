﻿namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Nestedscrollrect
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class Nestedscrollrect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        /// <summary>
        /// 外层被拦截需要正常拖动的ScrollRect，可不指定，默认在父对象中找
        /// </summary>
        public ScrollRect anotherScrollRect;
        /// <summary>
        /// 当前的ScrollRect（本脚本所放置的物体上）的拖动方向默认为上下拖动，否则为左右拖动型
        /// </summary>
        public bool thisIsUpAndDown = false;

        private ScrollRect thisScrollRect;

        void Awake()
        {
            thisScrollRect = GetComponent<ScrollRect>();
            if (anotherScrollRect == null)
                anotherScrollRect = GetComponentsInParent<ScrollRect>()[1];
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            anotherScrollRect.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            anotherScrollRect.OnDrag(eventData);
            float angle = Vector2.Angle(eventData.delta, Vector2.up);
            //判断拖动方向，防止水平与垂直方向同时响应导致的拖动时整个界面都会动
            if (angle > 45f && angle < 135f)
            {
                thisScrollRect.enabled = !thisIsUpAndDown;
                anotherScrollRect.enabled = thisIsUpAndDown;
            }
            else
            {
                anotherScrollRect.enabled = !thisIsUpAndDown;
                thisScrollRect.enabled = thisIsUpAndDown;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            anotherScrollRect.OnEndDrag(eventData);
            anotherScrollRect.enabled = true;
            thisScrollRect.enabled = true;
        }

    }
}
