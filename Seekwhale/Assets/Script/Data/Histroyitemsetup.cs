using UnityEngine.UI;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Histroyitemsetup
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Histroyitemsetup : MonoBehaviour
    {
        public Image trackimage;
        public Text title;
        public Text description;
        public Text time;

        public Button itemactionbtn;

        public void Histroysetup(string _trackimageurl, string _title,string _description, string _time)
        {
            trackimage.sprite = Settrackimage(_trackimageurl);
            title.text = _title;
            description.text = _description;
            time.text = _time;      
            
            //添加按钮事件                
        }



        private Sprite Settrackimage(string _trackimageurl)
        {
            Texture2D t2d = new Texture2D(128, 128, TextureFormat.RGB24, false);
            t2d.LoadImage(Fileoperation.Getfilefromurl(_trackimageurl));
            return Sprite.Create(t2d, new Rect(0, 0, 128, 128), new Vector2(0.5f,0.5f), 100);
        }       
    }
}
