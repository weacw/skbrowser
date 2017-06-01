using System;
using Mono.Data.Sqlite;
using UnityEngine.UI;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Favoriteview
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Favoriteview : Baseview
    {
        public Button backbtn;
        public GameObject favoritetemprefab;
        public RectTransform favoritelistroot;
        internal Favoriteoperation favoriteoperation;
        internal Dictionary<int, GameObject> favoritegameobjects = new Dictionary<int, GameObject>();
        public override void Bindingeventstobtn()
        {
            backbtn.onClick.AddListener(() =>
              {
                  Uistack.Getinstance().Return();
              });
        }

        public override void Initview()
        {
            base.Initview();
            favoriteoperation = GetComponent<Favoriteoperation>();
            callback = Creatfavoriteitem;

        }

        private void Creatfavoriteitem()
        {
            favoriteoperation.Getdataformsqlite(Waitsqlitereadingends);
        }

        private void Waitsqlitereadingends()
        {
            foreach (KeyValuePair<int, Item> item in favoriteoperation.favorite)
            {
                //不重复创建已经创建出来的Gameobject对象
                if (favoritegameobjects.ContainsKey(item.Key)) continue;

                //创建favorite Gameobject对象
                GameObject tmp = Instantiate(favoritetemprefab, favoritelistroot);
                RectTransform rect = tmp.GetComponent<RectTransform>();
                rect.localScale = Vector3.one;
                rect.localRotation = Quaternion.identity;
                rect.localPosition = Vector3.zero;

                tmp.GetComponent<Itemsetup>().item = item.Value;

                //将创建出来的favorite Gameobject 存储起来以便管理            
                favoritegameobjects.Add(item.Key, tmp);
            }
        }

        private int MappingSqlitedatareader(string _mappingname, SqliteDataReader _sqlitedatareader)
        {
            return _sqlitedatareader.GetOrdinal(_mappingname);
        }
    }
}
