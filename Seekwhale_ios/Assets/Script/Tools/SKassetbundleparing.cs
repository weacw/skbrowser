namespace SeekWhale
{
    using System;
    /*
    * 功 能： 仅用于加载解析assetbundle资源
    * 类 名： SKassetbundleparing
    * Email: paris3@163.com
    * 作 者： NSWell
    * Copyright (c) SeekWhale. All rights reserved.
*/

    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    public delegate void CallBack(UnityEngine.Object obj);

    public class SKassetbundlehelper
    {

        public static readonly SKassetbundlehelper instance = new SKassetbundlehelper();
        internal WWW bundlewww;
        /// <summary>
        /// 解析assetbundle
        /// </summary>
        /// <param name="_url">assetbundle地址</param>
        /// <param name="_version"></param>
        /// <param name="_successcallback"></param>
        /// <returns></returns>
        public IEnumerator Paringbundle(string _url, int _version, CallBack _successcallback)
        {
            if (!Browser.Getinstance().GetNetreachable(false))
            {
                yield break;
            }
            //下载assetbundle至内存并保存
            bundlewww = WWW.LoadFromCacheOrDownload(_url, _version);
            yield return bundlewww;
            if (!string.IsNullOrEmpty(bundlewww.error))
            {
                Debug.LogError("\n ERROR:  " + bundlewww.error + "\n");
                yield break;
            }

            //创建异步加载assetbundle请求
            AssetBundleRequest request = bundlewww.assetBundle.LoadAllAssetsAsync();

            while (!request.isDone)
                yield return null;

            //assetbundle请求加载完毕后调用回调以便执行实例化对象
            if (_successcallback != null)
                _successcallback.Invoke(request.allAssets[0] as GameObject);

            bundlewww.assetBundle.Unload(false);

            //释放缓存
            bundlewww.Dispose();
            bundlewww = null;
        }

        /// <summary>
        /// 检查当前资源是否缓存
        /// </summary>
        /// <param name="_url">资源地址</param>
        /// <param name="_version">资源版本</param>
        /// <returns></returns>
        public bool Checkcache(string _url, int _version)
        {
            return Caching.IsVersionCached(_url, _version);
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Cleacach()
        {
            Caching.CleanCache();
        }

        public long Getcachesize()
        {
            return Caching.spaceOccupied;
        }
    }
}
