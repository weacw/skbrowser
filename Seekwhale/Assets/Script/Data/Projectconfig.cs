namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Projectconfig
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Projectconfig : ScriptableObject
    {
        //--About project
        public string projectname;
        public string projectversion;
        public string projectdescription;
        public string companycopyright;

        //--About default asset
        public Texture2D defaultvideothumbnial;
        public Texture2D defaultitemthumbnail;

        //--About default path config
        public string appenvironmentalpath;
        public string marklesspath;
        public string assetmodulspath;
        public string recordingpath;
        public string photopath;
        public string databasepath;


        private const string photofolder = "photo";
        private const string recordingfolder = "record";
        private const string assetmodulsfolder = "asset";
        private const string databasefolder = "database";
        private const string marklessfolder = "marklessimage";

        /// <summary>
        /// 设置项目文件夹、路径等
        /// </summary>
        public void Setup()
        {

           switch (Application.platform)
            {
                case RuntimePlatform.Android:
                case RuntimePlatform.IPhonePlayer:
                    appenvironmentalpath = Application.persistentDataPath + "/" + projectname + "/";
                    break;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                    string tmp = Application.dataPath;
                    int index = tmp.IndexOf("Asset");
                    tmp.Substring(0, index);
                    appenvironmentalpath = tmp + "/" + projectname + "/";
                    break;
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.WindowsPlayer:
                    appenvironmentalpath = Application.dataPath + "/" + projectname + "/";
                    break;
            }
            //获取不同平台需要获取的相册路径        
            photopath = Createforld(Crossplatformbridge.Getinstance().Getpath()+"/"+projectname);
            recordingpath = Createforld(System.IO.Path.Combine(appenvironmentalpath, recordingfolder));
            assetmodulspath = Createforld(System.IO.Path.Combine(appenvironmentalpath, assetmodulsfolder));
            databasepath = Createforld(System.IO.Path.Combine(appenvironmentalpath, databasefolder));
            marklesspath = Createforld(System.IO.Path.Combine(appenvironmentalpath, marklessfolder));
        }


        /// <summary>
        /// 查询路径下的文件夹是否存在，若不存在则创建路径下的文件夹
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        private string Createforld(string _path)
        {
            Debug.Log(_path);
            if (!System.IO.Directory.Exists(_path))
                System.IO.Directory.CreateDirectory(_path);
            return _path;
        }
    }
}
