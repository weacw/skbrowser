using System;
using UnityEngine.UI;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Histroyview
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Histroyview : Baseview
    {
        public Button back;
        public Button trash;

        public GameObject histroyitemprefab;
        public RectTransform histroylistroot;

        private Histroyoperation histroyoperation;


        public override void Bindingeventstobtn()
        {
            back.onClick.AddListener(() =>
            {
                Uistack.Getinstance().Return();
            });
            callback = Creathistroyitem;
        }

        public override void Initview()
        {
            base.Initview();
            originaloffset = self.anchoredPosition;
            histroyoperation = FindObjectOfType<Histroyoperation>();
        }


        public override void Updateviewstatus(Viewstatus _viewstatus)
        {
            base.Updateviewstatus(_viewstatus);
        }



        private void Creathistroyitem()
        {
            histroyoperation.Getdataformsqlite(Waitsqlitereadingends);

        }


        private void Waitsqlitereadingends()
        {
            for (int i = 0; i < histroyoperation.histroy.Count; i++)
            {
                GameObject tmp = Instantiate(histroyitemprefab,histroylistroot);
                RectTransform rect = tmp.GetComponent<RectTransform>();
                rect.localScale = Vector3.one;
                rect.localRotation = Quaternion.identity;
                rect.localPosition = Vector3.zero;               
                tmp.GetComponent<Histroyitemsetup>().Histroysetup(histroyoperation.histroy[i].thumbnails, 
                    histroyoperation.histroy[i].itemname, histroyoperation.histroy[i].description, DateTime.Now.ToString("yyyy-M-d"));

            }
        }
    }
}
