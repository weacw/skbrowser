using System;
using Mono.Data.Sqlite;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Favoriteoperation
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Favoriteoperation : MonoBehaviour
    {       
        public void Adddatatosqlite(List<string> _datas)
        {
            Sqlitehelper helper = new Sqlitehelper("data source=skbrowser.db");
            helper.InsertInto("favorite", _datas.ToArray());
            helper.CloseSqlConnection();
        }
        public void Deletedatafromsqlite(string _id)
        {
            Sqlitehelper helper = new Sqlitehelper("data source=skbrowser.db");
            helper.Delete("favorite", "id", _id);
        }
        public Dictionary<int,Item> favorite = new Dictionary<int,Item>();

        public void Getdataformsqlite(Action _action)
        {
            Sqlitehelper helper = new Sqlitehelper("data source=skbrowser.db");
            SqliteDataReader sdr = helper.ReadFullTable("favorite_test");
            while (sdr.Read())
            {
                Item item = new Item();
                int id = sdr.GetInt16(MappingSqlitedatareader("id", sdr));

                //不重复为已存在收藏表（数据库）中的数据进行解析
                if (favorite.ContainsKey(id)) continue;
                Debug.Log(sdr.GetString(MappingSqlitedatareader("jsondata", sdr)));
                item =JsonUtility.FromJson<Item>(sdr.GetString(MappingSqlitedatareader("jsondata", sdr)));            
                favorite.Add(item.id,item);
            }
            helper.CloseSqlConnection();

            if (_action != null) _action.Invoke();
        }
        private int MappingSqlitedatareader(string _mappingname, SqliteDataReader _sqlitedatareader)
        {
            return _sqlitedatareader.GetOrdinal(_mappingname);
        }
    }
}
