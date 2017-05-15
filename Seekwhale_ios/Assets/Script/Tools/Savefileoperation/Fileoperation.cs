using System;
using System.IO;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Fileoperation
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class Fileoperation
    {

        /// <summary>
        /// 将二进制文件以某种格式存储到本地存储卡的指定路径上
        /// </summary>
        /// <param name="_savedpath">存储路径</param>
        /// <param name="_filename">文件名称以及文件格式后缀名</param>
        /// <param name="_filebytes">文件二进制数组</param>
        public static void Writefiletodisk(string _savedpath, string _filename, byte[] _filebytes, Action _callback = null)
        {
            if (!Directory.Exists(_savedpath))
                Directory.CreateDirectory(_savedpath);
            string tmppath = Path.Combine(_savedpath, _filename);
            File.WriteAllBytes(tmppath, _filebytes);
            if (_callback != null)
                _callback.Invoke();
        }

        public static void Writefiletodisk(string _sourcefile, string _targetpath, string _filename, Action _callback = null)
        {
            if (!Directory.Exists(_targetpath))
                Directory.CreateDirectory(_targetpath);
            byte[] bytes = Filetobytes(_sourcefile);
            Debug.Log(bytes.Length);
            File.WriteAllBytes(_targetpath + "/" + _filename, bytes);
            if (_callback != null)
                _callback.Invoke();
        }

        public static byte[] Filetobytes(string _path)
        {
            return File.ReadAllBytes(_path);
        }

        public static string Generatefilename(string _endwith)
        {
            string filename = null;
            string tmpfilename = DateTime.Now.ToString("F");
            string[] array = tmpfilename.Split(',', ' ', ':');
            foreach (string str in array)
            {
                filename += str;
            }
            return filename + _endwith;
        }
    }
}
