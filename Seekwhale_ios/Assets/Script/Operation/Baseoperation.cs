using System.Collections.Generic;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Baseoperation
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/
    using UnityEngine;

    public abstract class Baseoperation : MonoBehaviour
    {

        //public Dictionary<string,object> parameters = new Dictionary<string, object>();

        //资源对象
        public GameObject Getoperattarget { get; protected set; }

        //资源配置
        public Contentconfig config;

        //资源所属行业类型
        public Datatype datatype;

        //自身transform
        protected Transform selftransform;

        //初始化已完成
        protected bool isinited;


        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Start()
        {
            Initop();
            selftransform = this.transform;
        }
        /// <summary>
        /// 执行逻辑操作
        /// </summary>
        public abstract void Dooperation();

        /// <summary>
        /// 初始化 'Dooperation' 所需参数
        /// </summary>
        public virtual void Initop()
        {
            Scannermanager.Getinstance().baseoperation = this;
            Getoperattarget = this.gameObject;
            isinited = true;
        }

        /// <summary>
        /// 脚本禁用后的回调,用于释放资源等
        /// </summary>
        protected virtual void OnDisable()
        {
            selftransform = null;
            Getoperattarget = null;
            Scannermanager.Getinstance().baseoperation = null;
            isinited = false;
        }


        /// <summary>
        /// 脚本被启动的回调
        /// </summary>
        protected virtual void OnEnable()
        {
            
        }
    }
}
