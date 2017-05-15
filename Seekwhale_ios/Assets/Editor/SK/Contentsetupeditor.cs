namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Contentsetupeditor
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    public class Contentsetupeditor : EditorWindow
    {
        private static Contentsetupeditor contentsetupedior;
        private Contentconfig config;
        private string fullpath;
        private string tmppath = "The path is empty!";
        [MenuItem("Tools/SK-Browers/Content config editor")]
        private static void Init()
        {
            contentsetupedior = GetWindow<Contentsetupeditor>();
            contentsetupedior.Show();
        }
        private static GUIContent Getguicontent(string _content, string _tooltips, Texture _t2d)
        {
            return new GUIContent(_content, _t2d, _tooltips);
        }

        private void OnGUI()
        {
            if (contentsetupedior == null)
                Init();
            else
                contentsetupedior.Show();

            if (config == null)
                config = ScriptableObject.CreateInstance<Contentconfig>();

            EditorGUILayout.BeginVertical("HelpBox");
            EditorGUILayout.Space();
            //-------------------------------Eenum-----------------------------------------------------------------------
            config.bundletype = (Bundletype)EditorGUILayout.EnumPopup("Bundle type", config.bundletype);
            config.bundlelosetype = (Bundlelosetype)EditorGUILayout.EnumPopup("Bundle lose type", config.bundlelosetype);
            EditorGUILayout.Space();
            //-------------------------------Vector-----------------------------------------------------------------------
            config.bundleposition = EditorGUILayout.Vector3Field(Getguicontent("Bundle Position", null, null), config.bundleposition);
            config.bundlerotation = EditorGUILayout.Vector3Field(Getguicontent("Bundle Rotation", null, null), config.bundlerotation);
            config.bundlescale = EditorGUILayout.Vector3Field(Getguicontent("Bundle Scale", null, null), config.bundlescale);
            if (config.bundlelosetype == Bundlelosetype.SCREEN2D)
                config.bundleposistionoffset = EditorGUILayout.Vector3Field(Getguicontent("Bundle posistion offset", null, null), config.bundleposistionoffset);
            //-------------------------------Boolean----------------------------------------------------------------------
            config.gestureoperation = EditorGUILayout.Toggle("Gesture operation", config.gestureoperation);
            //-------------------------------Button-----------------------------------------------------------------------
            GUI.color = Color.red;
            EditorGUILayout.BeginHorizontal("HelpBox");
            if (!string.IsNullOrEmpty(fullpath))
                EditorGUILayout.LabelField("Path: " + fullpath);
            else
                EditorGUILayout.LabelField("Path: " + tmppath);

            if (GUILayout.Button("Select folder", "PreButton"))
            {
                fullpath = EditorUtility.SaveFilePanel("Create config", "", config.GetInstanceID().ToString(), "asset");
            }
            EditorGUILayout.EndHorizontal();
            GUI.color = Color.white;

            if (GUILayout.Button("Save config"))
            {
                if (string.IsNullOrEmpty(fullpath)) return;
                int id = fullpath.IndexOf("Asset");
                string shortpath = fullpath.Substring(id, fullpath.Length - id);
                AssetDatabase.CreateAsset(config, shortpath);
                EditorUtility.SetDirty(config);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh(ImportAssetOptions.Default);
                EditorGUIUtility.PingObject(config);
                config = null;
            }
            EditorGUILayout.EndVertical();
        }
    }
}
