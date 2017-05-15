using System.Collections.Generic;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Categroy
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/
    [System.Serializable]
    public class Categroy
    {
        public string id;
        public string category;
        public string alias;
        public string description;
    }
    [System.Serializable]
    public class Categroylist
    {
        public List<Categroy> result = new List<Categroy>();
    }
}
