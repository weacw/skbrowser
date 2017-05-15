namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Userinputoption
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using UnityEngine;

    public class Userinputoption
    {
        public bool canop;
        public GameObject result;
        public delegate void Onclicked(GameObject _target);
        public  event Onclicked Onclickedhandler;
        public void Ontriggerevent()
        {
            if (Onclickedhandler != null)
                Onclickedhandler.Invoke(result);
        }
    }
}
