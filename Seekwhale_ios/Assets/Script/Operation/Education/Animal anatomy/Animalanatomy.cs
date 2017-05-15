namespace SeekWhale
{
    using System;
    /*
* 功 能： N/A
* 类 名： Animalanatomy
* Email: paris3@163.com
* 作 者： NSWell
* Copyright (c) SeekWhale. All rights reserved.
*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Animalanatomy : Educationop
    {
        public GameObject ui;
        public List<Baserendererop> orrrenderersop = new List<Baserendererop>();


        private RenderingMode renderingmode;
        private Opshader op = null;




        public void Creatbtn(string name)
        {
            ui.SetActive(true);
        }

        public void Showskin()
        {
            renderingmode = RenderingMode.OPAQUE;
            Dooperation();
        }


        public void Hideskin()
        {
            renderingmode = RenderingMode.TRANSPARENT;
            Dooperation();
        }

        public override void Initop()
        {
            base.Initop();
            Renderer[] rds = Getoperattarget.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < orrrenderersop.Count; i++)
            {
                orrrenderersop[i].nodename = orrrenderersop[i].renderer.gameObject.name;
                foreach (Material mat in orrrenderersop[i].renderer.materials)
                {
                    orrrenderersop[i].maincolor = mat.color;
                    orrrenderersop[i].metallic = mat.GetFloat("_Metallic");
                    orrrenderersop[i].smoothness = mat.GetFloat("_Glossiness");
                }
            }
            op = new Opshader();
        }


        public override void Dooperation()
        {
            for (int i = 0; i < orrrenderersop.Count; i++)
            {
                op.Setmaterialrenderingmode(orrrenderersop[i].renderer.materials[i], renderingmode, orrrenderersop[i], null);
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void OnDisable()
        {
            //if (op != null) op = null;
            //if (isinited) isinited = false;
            //if (orrrenderersop.Count > 0)
            //{
            //    orrrenderersop.Clear();
            //    orrrenderersop = null;
            //}
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void OnEnable()
        {
            if (!isinited)
                Initop();
        }
    }
}
