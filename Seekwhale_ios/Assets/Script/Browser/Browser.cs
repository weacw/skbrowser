using System;
using Boo.Lang;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Browser
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using UnityEngine;

    public class Browser : Singleton<Browser>
    {
        internal int bundleversion;
        protected IEnumerator browser_accesssql;
        protected IEnumerator browser_download;
        protected bool netreachable;
        protected const float TIME_OUT = 5;


        public bool GetNetreachable(bool _showtip)
        {
            if (!netreachable && _showtip)
                Uimanager.Getinstance().Showtips("网络不可用");
            return netreachable;
        }
        internal Action<string> errorcallback;
        public WWW Getaccesssqlwww
        {
            get;
            protected set;
        }

        private void Awake()
        {
            GetNetreachable(true);
            Debug.Log(Application.internetReachability);
              switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
                    //   Uimanager.Getinstance().Showtips("当前网络不可用");
                    netreachable = false;
                    break;
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                    //           Uimanager.Getinstance().Showtips("当前网络为3G/4G");
                    netreachable = true;
                    break;
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    //      Uimanager.Getinstance().Showtips("已连接到WIFI");
                    netreachable = true;
                    break;
            }
        }

        private void OnDisable()
        {
            if (browser_accesssql != null)
                StopCoroutine(browser_accesssql);
            if (browser_download != null)
                StopCoroutine(browser_download);
        }

        /// <summary>
        /// 浏览器访问服务器
        /// </summary>
        /// <param name="_successcallback">浏览器访问服务器后返回数据的处理方式</param>
        /// <returns></returns>
        private IEnumerator Accesstheserver(Action<string> _successcallback, Action<string> _failederrorcallback)
        {
            yield return Getaccesssqlwww;
            if (!string.IsNullOrEmpty(Getaccesssqlwww.error))
            {
                Debug.LogError("ERROR:" + Getaccesssqlwww.error);
                if (_failederrorcallback != null)
                    _failederrorcallback.Invoke(Getaccesssqlwww.error);
                //TODO:网络连接错误提示
                yield break;
            }
            while (!Getaccesssqlwww.isDone)
            {
                yield return null;
            }
#if UNITY_EDITOR
            Debug.Log("BROWSER DEBUG: " + Getaccesssqlwww.text);
#endif
            //请求发起成功并得到返回数据的回调
            if (_successcallback != null)
                _successcallback.Invoke(Getaccesssqlwww.text);
            Getaccesssqlwww.Dispose();
            Getaccesssqlwww = null;
            browser_accesssql = null;
        }

        private IEnumerator Downloadasset(WWW _www, Action<byte[], object> _successcallback, Action<string> _failederrorcallback, object _object)
        {
            yield return _www;
            if (!string.IsNullOrEmpty(_www.error))
            {
                Debug.LogError("ERROR:" + _www.error);
                if (_failederrorcallback != null)
                    _failederrorcallback.Invoke(_www.error);
                //TODO:网络连接错误提示
                yield break;
            }
            while (!_www.isDone)
            {
                yield return null;
            }
            //#if UNITY_EDITOR
            //            Debug.Log("BROWSER DEBUG: " + _www.text);
            //#endif
            //请求发起成功并得到返回数据的回调
            if (_successcallback != null)
                _successcallback.Invoke(_www.bytes, _object);
            _www.Dispose();
            browser_download = null;
        }
        /// <summary>
        /// 需要提交给服务器的基本表单
        /// </summary>
        /// <returns></returns>
        private static WWWForm Getwwwform()
        {
            WWWForm wwwform = new WWWForm();
            wwwform.AddField(browserdata.post_db_user, browserdata.db_user);
            wwwform.AddField(browserdata.post_db_name, browserdata.db_name);
            wwwform.AddField(browserdata.post_db_passworld, browserdata.db_passworld);
            return wwwform;
        }

        /// <summary>
        /// 启用浏览器
        /// </summary>
        private void Openbrowser(string _url, WWWForm _wwwform, Action<string> _successcallback, Action<string> _failederrorcallback)
        {
            if (!GetNetreachable(true))
            {
                return;
            }



            if (browser_accesssql == null)
                browser_accesssql = Accesstheserver(_successcallback, _failederrorcallback);
            Getaccesssqlwww = new WWW(_url, _wwwform);
            StartCoroutine(browser_accesssql);
        }

        private void Openbrowser(string _url, Action<byte[], object> _successcallback, Action<string> _failederrorcallback, object _object)
        {
            if (!GetNetreachable(true))
            {
                return;
            }

            
            browser_download = Downloadasset(new WWW(_url),_successcallback, _failederrorcallback, _object);
            StartCoroutine(browser_download);
        }
        private void Openbrowser(string _url, Action<string> _successcallback, Action<string> _failederrorcallback)
        {
            if (!GetNetreachable(true))
            {
                return;
            }


            browser_download = Accesstheserver(_successcallback, _failederrorcallback);
            StartCoroutine(browser_download);
        }

        public void Connectoserver(Action<string> _successcallback, Action<string> _failederrorcallback)
        {
            Openbrowser(browserdata.querysingletable,  _successcallback, _failederrorcallback);
        }

        public void Getitems(string _trackerid, Action<string> _successcallback, Action<string> _failederrorcallback)
        {
            var wwwform = Getwwwform();
            wwwform.AddField(browserdata.post_db_table_name, browserdata.table_items);
            wwwform.AddField(browserdata.post_db_tracker_id, _trackerid);
            Openbrowser(browserdata.querysingletable, wwwform, _successcallback, _failederrorcallback);
        }

        public void Getitemlist(Action<string> _successcallback, Action<string> _failederrorcallback)
        {
            var wwwform = Getwwwform();
            wwwform.AddField(browserdata.post_db_table_name, browserdata.table_items);
            Openbrowser(browserdata.queryentiretable, wwwform, _successcallback, _failederrorcallback);
            Debug.Log("Get item");
        }

        public void Getcatergory(Action<string> _successcallback, Action<string> _failederrorcallback)
        {
            var wwwform = Getwwwform();
            wwwform.AddField(browserdata.post_db_table_name, browserdata.table_category);
            Openbrowser(browserdata.queryentiretable, wwwform, _successcallback, _failederrorcallback);
        }

        public void GetMarkless(string _url, Action<byte[], object> _successcallback, Action<string> _failederrorcallback, object _object)
        {
            Openbrowser(_url, _successcallback, _failederrorcallback, _object);
        }

        internal T Getdatafromjson<T>(string _json)
        {
            return JsonUtility.FromJson<T>(_json);
        }
    }
}
