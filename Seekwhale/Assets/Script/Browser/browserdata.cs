namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： browserdata
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class browserdata
    {
        public const string queryentiretable = "http://192.168.10.102/sk/Queryentiretable.php";
        public const string querysingletable = "http://192.168.10.102/sk/Querysingletable.php";

        public const string table_category = "category";
        public const string table_items = "items";

        //-----------------Variable name------------------------------------------------
        public const string post_db_name = "DBNAME";
        public const string post_db_user = "DBUSER";
        public const string post_db_passworld = "DBPSD";
        public const string post_db_table_name = "TABLENAME";
        public const string post_db_tracker_id = "TRACKERID";
        //-----------------Variable data-------------------------------------------------
        public const string db_name = "skdb_skbowser";
        public const string db_user = "root";
        public const string db_passworld = "";
    }
}
