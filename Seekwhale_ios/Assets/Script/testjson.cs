namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： testjson
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class testjson : MonoBehaviour
    {

        public string jsons = @"{""items"":[{""id"": ""1"",""category"": ""products"",""alias"": ""产品展示"",""description"": ""虚实结合，立体、具象、生动地展示产品，带给消费者震撼感官体验""},{""id"": ""2"",""category"": ""marketing"",""alias"": ""营销推广"",""description"": ""颠覆传统交互体验，升级体验营销形式""},{""id"": ""3"",""category"": ""merchandise"",""alias"": ""促成交易"",""description"": ""与支付宝微信等平台打通，实现一键式多平台促成交易""}]}";
        // Use this for initialization
        void Start()
        {
            //List<myjson> list = new List<myjson>();
            //list.Add(new myjson());
            //list.Add(new myjson());
            //Debug.Log(JsonUtility.ToJson(new myjson()));
            //Debug.Log(JsonUtility.FromJson<test>(jsons).items[0].description);
        }

        public void TEST(string _json)
        {
            Debug.Log(Browser.Getinstance().Getdatafromjson<Categroylist>(_json).result[0].alias);
        }

    }



}
