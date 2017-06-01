namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： TESTABS
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TESTABS : MonoBehaviour
    {
        public string loadPath = "TEST_abs\\";
        public string filename;

        private string path;

       

        public void LAOD()
        {
            Caching.CleanCache();
            Debug.Log(Application.streamingAssetsPath);
            if (Application.platform == RuntimePlatform.Android)
                path = "jar:file://" + Application.dataPath + "!/assets/" + loadPath + filename;
            else
                path = "file://"+Application.streamingAssetsPath+"/" + loadPath + filename;
            Debug.Log("--------\n");
            StartCoroutine(SKassetbundlehelper.instance.Paringbundle(path,0,CallBack));
            Debug.Log("--------\n");

            Debug.Log(Caching.spaceOccupied / 1048576);
        }
        private void CallBack(UnityEngine.Object obj)
        {
            Debug.Log(obj.name);
            GameObject.Instantiate((GameObject)obj);
            Debug.Log(Caching.spaceOccupied / 1048576);

        }

        private void Start()
        {
            //创建数据库名称为xuanyusong.db
            Sqlitehelper db = new Sqlitehelper("data source=xuanyusong.db");

            //创建数据库表，与字段
            db.CreateTable("momo", new string[] { "name", "qq", "email", "blog" }, new string[] { "text", "text", "text", "text" });
            //关闭对象
            db.CloseSqlConnection();
        }
    }
}
