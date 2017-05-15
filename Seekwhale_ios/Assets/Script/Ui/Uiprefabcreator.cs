namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Uiprefabcreator
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Uiprefabcreator
    {
        public  static GameObject Uicreator(Transform _targetroot, GameObject _prefab)
        {
            GameObject tmp = Object.Instantiate(_prefab, _targetroot);
            RectTransform  rect =tmp.GetComponent<RectTransform>();
            rect.anchoredPosition3D = Vector3.zero;
            rect.localScale = Vector3.one;
            rect.localEulerAngles = Vector3.zero;
            return tmp;
        }
    }
}
