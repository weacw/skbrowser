namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： SKAssetbundlecreator
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class SKAssetbundlecreator
    {
        public static void BuildABs(string _savepath, string _resourcesasset, BuildTarget _platform)
        {
            List<string> resourcesAssets = new List<string>();
            AssetBundleBuild[] buildMap = new AssetBundleBuild[2];
            string filename = System.IO.Path.GetFileName(_savepath);

            buildMap[0].assetBundleName = filename;

            resourcesAssets.Add(_resourcesasset);
            buildMap[0].assetNames = resourcesAssets.ToArray();

            int id = _savepath.IndexOf("Asset");
            string shortpath = _savepath.Substring(id, _savepath.Length - id).Replace(filename, "");


            BuildPipeline.BuildAssetBundles(shortpath, buildMap, BuildAssetBundleOptions.ChunkBasedCompression|BuildAssetBundleOptions.CollectDependencies, _platform);
            AssetDatabase.Refresh();
        }
        public static void TEST()
        {

        }
    }
}
