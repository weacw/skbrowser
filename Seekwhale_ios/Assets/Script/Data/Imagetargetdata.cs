namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Imagetargetdata
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DG.Tweening;
    public class Imagetargetdata : MonoBehaviour
    {
        [Space]
        //public GameObject prefab;
        public Bundlelosetype losetype;

        [Header("User interaction settings")]
        public bool gestureinteraction;
        public bool physicsinput;


        [Header("Set the parameters when instantiate")]
        public Vector3 posistion = Vector3.zero;
        public Vector3 scale = Vector3.one;
        public Vector3 rotation = Vector3.zero;
        [Header("Set the parameters when losetype is center")]
        public float gotocenterrate = 10;
        public Vector3 centeroffset;
        public Vector3 centerrotation;
           [HideInInspector]
        public GameObject Instantiatedobject;

        private Transform imagetransform;
        private Transform instantiatedtransform;
        private IEnumerator coroutine;
        private Tweener tweener;
        public void Releaseinstantiatedtarget()
        {
            if (Instantiatedobject != null)
            {
                imagetransform.SetParent(null);
                Destroy(Instantiatedobject);
            }
        }
        public void Hideinstantiatedobject()
        {
            if (Instantiatedobject != null)
                Instantiatedobject.SetActive(false);
        }

        public void Updatetocenter()
        {
            if (Instantiatedobject == null) return;
            imagetransform = Instantiatedobject.transform.root;
            imagetransform.SetParent(Camera.main.transform.parent);
            imagetransform.localEulerAngles = centerrotation;
            tweener= imagetransform.DOLocalMove(centeroffset + Vector3.zero, 1);
            tweener.OnComplete(() => { tweener.Kill(true); });
        }

        public void Brokenlink()
        {
            if (imagetransform == null) return;
            if (tweener != null)
                tweener.Kill(true);
            imagetransform.SetParent(null);
            imagetransform.localPosition = Vector3.zero;
            imagetransform.localEulerAngles = Vector3.zero;
        }
    }
}
