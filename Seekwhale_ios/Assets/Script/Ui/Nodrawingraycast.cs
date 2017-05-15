using UnityEngine.UI;

namespace SeekWhale
{
	/*
	* 功 能： N/A
	* 类 名： Nodrawingraycast
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Nodrawingraycast : Graphic {
	    public override void SetMaterialDirty()
	    {
	        base.SetMaterialDirty();
	    }

	    public override void SetVerticesDirty()
	    {
	        base.SetVerticesDirty();
	    }

	    protected override void OnPopulateMesh(VertexHelper vh)
	    {
	        vh.Clear();
	    }
	}
}
