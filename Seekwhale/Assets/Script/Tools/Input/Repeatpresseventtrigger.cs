using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Repeatpresseventtrigger
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Repeatpresseventtrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        public float interval = 0.1f;
        [SerializeField]UnityEvent Onlongpress = new UnityEvent();
        private bool ispointdown = false;



        public void OnPointerDown(PointerEventData eventData)
        {
            if (!ispointdown) return;
            Onlongpress.Invoke();
            ispointdown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ispointdown = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ispointdown = false;
        }
    }
}
