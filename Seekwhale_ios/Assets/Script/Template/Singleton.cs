namespace SeekWhale
{
	/*
	* 功 能： N/A
	* 类 名： Singleton
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Singleton<T> : MonoBehaviour where T:Singleton<T>    
    {
        private static T instance;
        public static T Getinstance()
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<T>();
                if (!instance)
                {
                    GameObject go = new GameObject("Singleton");
                    instance = go.AddComponent<T>();
                }
            }
            return instance;
        }

	}
}
