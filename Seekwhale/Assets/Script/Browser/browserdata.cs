namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Browserdata
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class Browserdata:ScriptableObject
    {
        public  string queryentiretable = "http://192.168.10.109/sk/Queryentiretable.php";
        public  string querysingletable = "http://192.168.10.109/sk/Querysingletable.php";

        public  string table_category = "category";
        public  string table_items = "items";

        //-----------------Variable name------------------------------------------------
        public  string post_db_name = "DBNAME";
        public  string post_db_user = "DBUSER";
        public  string post_db_passworld = "DBPSD";
        public  string post_db_table_name = "TABLENAME";
        public  string post_db_tracker_id = "TRACKERID";
        //-----------------Variable data-------------------------------------------------
        public  string db_name = "skdb_skbowser";
        public  string db_user = "root";
        public  string db_passworld = "";
    }
}
