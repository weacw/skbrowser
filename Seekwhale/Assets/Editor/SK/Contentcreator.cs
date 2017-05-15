namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Contentcreator
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    public class Contentcreator : EditorWindow
    {

        internal static Contentcreator contentcreator;


        private string fullpath;
        private string tmppath;
        private BuildTarget platform = BuildTarget.Android;
        private List<Object> bundles = new List<Object>();
        private Vector2 scrollview;

        private string[] toolbarcontent = new string[] { "Content Creator", "Bundle Creator" };
        private int selected;

        [MenuItem("Tools/SK-Browers/Content Creator")]
        private static void Init()
        {
            contentcreator = GetWindow<Contentcreator>();
            contentcreator.Show();
        }


        private void OnGUI()
        {
            selected = GUILayout.Toolbar(selected, toolbarcontent);

            switch (selected)
            {
                case 0: Projectconfig();break;
                case 1: Bundlecreator(); break;
            }
        }

        protected void Projectconfig()
        {

            Projectconfig config = CreateInstance<Projectconfig>();
            if (!string.IsNullOrEmpty(fullpath))
                EditorGUILayout.LabelField("Path: " + fullpath);
            else
                EditorGUILayout.LabelField("Path: " + tmppath);

            if (GUILayout.Button("Select folder", "PreButton"))
            {
                fullpath = EditorUtility.SaveFilePanel("Create config", "", config.GetInstanceID().ToString(), "asset");
            }
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
        }
        protected void Bundlecreator()
        {
            EditorGUILayout.Space();
            if (GUILayout.Button("Select the storage directory", "PreButton"))
            {
                fullpath = EditorUtility.SaveFilePanel("Create config", "", fullpath, "sk");
            }

            EditorGUILayout.BeginHorizontal();
            platform = (BuildTarget)EditorGUILayout.EnumPopup("Platform", platform);
            if (GUILayout.Button("Building bundle", "minibutton"))
            {
                foreach (Object item in bundles)
                {
                    SKAssetbundlecreator.BuildABs(fullpath, AssetDatabase.GetAssetPath(item), platform);
                }
            }
            EditorGUILayout.EndHorizontal();

            Rect area = EditorGUILayout.BeginVertical("Helpbox", GUILayout.Height(EditorGUIUtility.currentViewWidth));
            if (bundles.Count <= 0)
            {
                string meg = null;
                var dragArea = GUILayoutUtility.GetRect(0f, 35f, GUILayout.ExpandWidth(true));

                GUIContent title = new GUIContent(meg);
                if (string.IsNullOrEmpty(meg))
                {
                    title = new GUIContent("Drag Object here from Project view to get the object");
                }
                GUI.Box(dragArea, title);
            }
            Drawobj();
            EditorGUILayout.EndVertical();
            Dragdroparea(area);
        }

        protected void Dragdroparea(Rect _droparea)
        {
            Event aEvent;
            aEvent = Event.current;
            GUI.contentColor = Color.white;
            UnityEngine.Object temp = null;


            switch (aEvent.type)
            {
                case EventType.dragUpdated:
                case EventType.dragPerform:
                    if (!_droparea.Contains(aEvent.mousePosition))
                    {
                        break;
                    }

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    if (aEvent.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        for (int i = 0; i < DragAndDrop.objectReferences.Length; ++i)
                        {
                            temp = DragAndDrop.objectReferences[i];
                            if (bundles.Contains(temp)) continue;
                            bundles.Add(temp);
                            AssetDatabase.Refresh();
                            if (temp == null)
                            {
                                break;
                            }
                        }
                    }
                    Event.current.Use();
                    break;
                default:
                    break;
            }
        }

        protected void Drawobj()
        {
            if (bundles == null) return;

            EditorGUILayout.BeginVertical();
            scrollview = EditorGUILayout.BeginScrollView(scrollview);

            string meg = null;
            Event aEvent;
            aEvent = Event.current;
            GenericMenu menu = new GenericMenu();
            for (int i = 0; i < bundles.Count; i++)
            {
                Texture2D t2d = AssetPreview.GetAssetPreview(bundles[i]);
                GUILayout.Box(t2d);
                Rect r = GUILayoutUtility.GetLastRect();
                Rect newrect = new Rect(r.width / 2 - bundles[i].name.Length * 2.5f, 2 + r.y, r.width, r.height);
                GUI.Label(newrect, bundles[i].name);
                if (aEvent.button.Equals(0) && GUILayoutUtility.GetLastRect().Contains(aEvent.mousePosition) && aEvent.type == EventType.mouseDown)
                {
                    EditorGUIUtility.PingObject(bundles[i]);
                }

                if (aEvent.type == EventType.ContextClick && GUILayoutUtility.GetLastRect().Contains(aEvent.mousePosition))
                {
                    menu.AddItem(new GUIContent("Remove"), false, Revmoeitem, bundles[i]);
                    menu.ShowAsContext();
                    aEvent.Use();
                }
            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();
        }

        private void Revmoeitem(object _obj)
        {
            bundles.Remove((Object)_obj);
        }

    }
}
