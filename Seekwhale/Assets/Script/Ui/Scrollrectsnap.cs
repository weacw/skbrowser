using DG.Tweening;
using UnityEngine.UI;

namespace SeekWhale
{
	/*
	* 功 能： N/A
	* 类 名： Scrollrectsnap
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

    public class Scrollrectsnap : MonoBehaviour
    {
        public RectTransform panel;
        public Button[] bttn;
        public RectTransform center;

        public float[] distance;
        public float[] distreposition;
        private bool dragging = false;
        private int bttndistance;
        private int minbuttonnum;
        private int bttnlength;

        void Start()
        {
            bttnlength = bttn.Length;
            distance =new float[bttnlength];
            distreposition = new float[bttnlength];


            //Get distance between buttons
            bttndistance = (int) Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x -
                                           bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
        }

        void Update()
        {
            for (int i = 0; i < bttn.Length; i++)
            {
                distance[i] = Mathf.Abs(center.transform.position.x - bttn[i].transform.position.x);
                //distreposition[i] = center.GetComponent<RectTransform>().position.x -
                //                    bttn[i].GetComponent<RectTransform>().position.x;
                //distance[i] = Mathf.Abs(distreposition[i]);

                //if (distreposition[i] > 1200)
                //{
                //    float curx = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                //    float cury = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;
                //    Vector2 newanchoredpos = new Vector2(curx+(bttnlength*bttndistance),cury);
                //    bttn[i].GetComponent<RectTransform>().anchoredPosition = newanchoredpos;
                //}

                //if (distreposition[i] < -1200)
                //{
                //    float curx = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                //    float cury = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;
                //    Vector2 newanchoredpos = new Vector2(curx - (bttnlength * bttndistance), cury);
                //    bttn[i].GetComponent<RectTransform>().anchoredPosition = newanchoredpos;
                //}
            }

            float mindistance = Mathf.Min(distance);
            for (int i = 0; i < bttn.Length; i++)
            {
                if (mindistance == distance[i])
                {
                    minbuttonnum = i;
                }
            }

            if (!dragging)
                Lerptobttn(minbuttonnum*-bttndistance);
        }

        void Lerptobttn(float _position)
        {
            float newx = Mathf.Lerp(panel.anchoredPosition.x, _position, Time.deltaTime * 25f);
            ////if (Mathf.Abs(newx) >= Mathf.Abs(_position) - 1f && Mathf.Abs(newx) <= Mathf.Abs(_position) + 1)
            ////{
            ////    Debug.Log(bttn[minbuttonnum].name);
            ////}
            Vector2 newposition = new Vector2(newx, panel.anchoredPosition.y);
            panel.anchoredPosition = newposition;
            //panel.DOAnchorPos(new Vector2(newx, 0), 1);
        }

        public void Statdrag()
        {
            dragging = true;
        }

        public void Enddrag()
        {
            dragging = false;
        }
    }
}
