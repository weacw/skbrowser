using System;
using Mono.Data.Sqlite;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Histroyoperation
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Histroyoperation : MonoBehaviour
    {
        public void Adddatatosqlite(List<string> _datas)
        {
            Sqlitehelper helper = new Sqlitehelper("data source=skbrowser.db");
            helper.InsertInto("histroy", _datas.ToArray());
            helper.CloseSqlConnection();
        }

        public void Deletedatafromsqlite(string _id)
        {
            Sqlitehelper helper = new Sqlitehelper("data source=skbrowser.db");
            helper.Delete("histroy", "id", _id);
        }

        public void Updatedatatosqlite(string _id)
        {
            Sqlitehelper helper = new Sqlitehelper("data source=skbrowser.db");
            //helper.UpdateInto()
        }
        public List<Item> histroy = new List<Item>();

        public void Getdataformsqlite(Action _action)
        {

            Sqlitehelper helper = new Sqlitehelper("data source=skbrowser.db");
            SqliteDataReader sdr = helper.ReadFullTable("histroy");
            while (sdr.Read())
            {
                Item item = new Item();
                item.id = sdr.GetInt16(MappingSqlitedatareader("id", sdr));
                item.bunletype =
                    (Bundletype)Enum.Parse(typeof(Bundletype), sdr.GetString(MappingSqlitedatareader("bundletype",sdr)));
                item.bundleurlandroid = sdr.GetString(MappingSqlitedatareader("bundleurlandroid", sdr));
                item.bundleurlios = sdr.GetString(MappingSqlitedatareader("bundleurlios", sdr));
                item.category = sdr.GetString(MappingSqlitedatareader("category", sdr));
                item.description = sdr.GetString(MappingSqlitedatareader("description", sdr));
                item.thumbnails = sdr.GetString(MappingSqlitedatareader("thumbnails", sdr));
                item.itemname = sdr.GetString(MappingSqlitedatareader("itemname", sdr));
                item.trackerid = sdr.GetString(MappingSqlitedatareader("trackerid", sdr));
                item.trackerurl = sdr.GetString(MappingSqlitedatareader("trackerurl", sdr));
                item.tutorthumbnail = sdr.GetString(MappingSqlitedatareader("tutorthumbnail", sdr));
                item.tutourl = sdr.GetString(MappingSqlitedatareader("tutourl", sdr));
                item.visiblc = bool.Parse(sdr.GetString(MappingSqlitedatareader("visible", sdr)));
                histroy.Add(item);
            }
            helper.CloseSqlConnection();

            if(_action!=null)_action.Invoke();
        }

        private int MappingSqlitedatareader(string _mappingname, SqliteDataReader _sqlitedatareader)
        {
            return _sqlitedatareader.GetOrdinal(_mappingname);
        }
        
    }
}
