namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Ibaseinput
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/
    using UnityEngine;

    public interface Ibaseinput
    {
        void Whenuserclicktoretuanresult(out GameObject _results);
        void Onrelease();
        void Modulinit(Userinputoption _option);
    }
}
