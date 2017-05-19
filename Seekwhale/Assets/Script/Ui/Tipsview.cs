using DG.Tweening;
using UnityEngine.UI;

namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Tipsview
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Tipsview : Baseview
    {
        public Text msgtext;

        public float waitseconds = 1f;



        private void Autohide()
        {
            StartCoroutine(Waittingtohide());
        }

        private IEnumerator Waittingtohide()
        {
            yield return new WaitForSeconds(waitseconds);
            Updateviewstatus(Viewstatus.HIDE);
            callback = null;
            Uimanager.Getinstance().tips.Remove(this.gameObject);
            Destroy(this.gameObject, 1f);
        }


        public override void Bindingeventstobtn()
        {

        }

        public override void Initview()
        {
            base.Initview();

            originaloffset = new Vector3(0, self.rect.height / 2);
            movementtoffset = new Vector3(0, -self.rect.height / 2);
            viewenabled = true;
        }

        public void Setupmsg(string _msg)
        {
            if (!isinited)
                Initview();
            msgtext.text = _msg;
            Updateviewstatus(Viewstatus.SHOW);
            Autohide();
        }

        public override void Updateviewstatus(Viewstatus _viewstatus)
        {
            if (Uimanager.Getinstance().tips.Count >= 2)
            {
                for (int i = 0; i < Uimanager.Getinstance().tips.Count - 1; i++)
                {
                    GameObject tmp = Uimanager.Getinstance().tips[i];
                    Uimanager.Getinstance().tips.RemoveAt(i);
                    Destroy(tmp);
                }
            }

            base.Updateviewstatus(_viewstatus);

        }
    }
}
