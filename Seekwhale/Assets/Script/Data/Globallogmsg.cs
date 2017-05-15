namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Globallogmsg
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Globallogmsg
    {
        public enum Registertype
        {
            REGISTER,
            UNREGISTER
        };
        public const string TRAKABLEEMPTY = "ERROR: 'TRACKABLEBEHAVIOUR' is empty!";
        private const string REGISTERTRACKABLE = "Register trackable behaviour-:";
        private const string UNREGISTERTRACKABLE = "Unregister trackable behaviour-:";
        public static void Getregistermsg(Registertype _registertype, string _name)
        {
            string tmp = null; 
            switch (_registertype)
            {
                case Registertype.REGISTER:
                    tmp ="<color=yellow>"+ REGISTERTRACKABLE + _name+"</color>";
                    break;
                case Registertype.UNREGISTER:
                    tmp = "<color=magenta>" + UNREGISTERTRACKABLE + _name+"</color>";
                    break;              
            }
            Debug.Log(tmp);
        }
    }
}
