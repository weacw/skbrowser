using System;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Uistack
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Uistack : Singleton<Uistack>
    {
        [SerializeField]
        private List<Baseview> viewstack = new List<Baseview>();


        private Baseview Push(Baseview _baseview)
        {
            viewstack.Add(_baseview);
            return _baseview;
        }

        private Baseview Pop()
        {
            if (viewstack.Count <= 0) return null;
            Baseview bv = viewstack[viewstack.Count - 1];
            viewstack.RemoveAt(viewstack.Count - 1);
            return bv;
        }

        public void Openview(Baseview _opview, Viewstatus _viewstatus)
        {
            Push(_opview).Updateviewstatus(_viewstatus);
        }

        public void Return(int depth = 1)
        {
            for (int i = 0; i < depth; i++)
            {
                Baseview bv_a = Pop();
                if (bv_a != null)
                    bv_a.Updateviewstatus(Viewstatus.HIDE);
            }
        }

        public int Getcurdepth()
        {
            return viewstack.Count;
        }
    }
}
