using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SeekWhale
{
    using DG.Tweening;
    using System;
    /*
* 功 能： N/A
* 类 名： Scanview
* Email: paris3@163.com
* 作 者： NSWell
* Copyright (c) SeekWhale. All rights reserved.
*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityStandardAssets.ImageEffects;

    public class Scanview : Baseview
    {
        public Blur blur;

        public GameObject scanning;
        public GameObject scaned;


        public GameObject toolsbar;
        public GameObject tips;

        public Image recordprogress;
        
        public Button back;
        public Button dropbardown;
        public Button dropbarup;
        public Button recording;
        public Button close;
        public Button dropdown;
        public Button dropup;
        public EventTrigger takephotoorrecord;

        public Slider progress;

        public RectTransform dropfunroot;
        public GameObject droparea;
        public GameObject dropareacenter;
        public GameObject[] dropfunsprefab;

        private List<GameObject> dropfuns = new List<GameObject>();
        private Tweener fade, scaleY, scaleall;
        public override void Bindingeventstobtn()
        {
            close.onClick.AddListener(() =>
            {
                FindObjectOfType<Imagetargettracker>().targetdata.Releaseinstantiatedtarget();
                if (Uistack.Getinstance().Getcurdepth() > 2)
                {
                    Cloudrecoeventhandler.Getinstance().Stopscanning();
                   // Uimanager.Getinstance().Addblackfade(true,null);
                    Uistack.Getinstance().Return(2);
                    FindObjectOfType<Detaildescriptionview>().detailsetup.enabled = true;
                }
                else
                {
                    Cloudrecoeventhandler.Getinstance().Restartscanning();
                    Updateviewstatus(Viewstatus.SHOW);
                }
            });



            back.onClick.AddListener(() =>
            {
                Cloudrecoeventhandler.Getinstance().Stopscanning();
                Uistack.Getinstance().Return(2); 
            });

            dropdown.onClick.AddListener(() => Onclickdropdown());
            dropup.onClick.AddListener(() => Onclickdropup());
            
            Takephotoorrecord.Getinstance().Recording.onrecorded = Onrecorded;
            Takephotoorrecord.Getinstance().Recording.onrecording = Updaterecordingprogress;
        }

        public override void Updateviewstatus(Viewstatus _viewstatus)
        {
            //base.Updateviewstatus(_viewstatus);
            switch (Scannermanager.Getinstance().Getcurrecostatus)
            {
                case Scanstatus.GOTOSCAN:
                    scaned.SetActive(false);
                    scanning.SetActive(true);
                    progress.gameObject.SetActive(false);
                    back.gameObject.SetActive(true);
                    if (blur.enabled)
                    {
                        //base.Updateviewstatus(_viewstatus);
                        blur.enabled = false;
                    }
                    break;
                case Scanstatus.SCANTHERESULTS:
                    scaned.SetActive(false);
                    scanning.SetActive(true);
                    progress.gameObject.SetActive(true);
                    back.gameObject.SetActive(false);
                    break;
                case Scanstatus.SCANISDONE:
                    scaned.SetActive(true);
                    scanning.SetActive(false);
                    progress.gameObject.SetActive(false);
                    back.gameObject.SetActive(false);
                    blur.enabled = false;
                    break;
                case Scanstatus.STOPSCANNING:
                    scaned.SetActive(false);
                    scanning.SetActive(true);
                    progress.gameObject.SetActive(false);
                    back.gameObject.SetActive(false);
                    blur.enabled = true;
                    base.Updateviewstatus(_viewstatus);
                    break;
            }
           base.Updateviewstatus(_viewstatus);
        }
        public override void Initview()
        {
            base.Initview();
            viewenabled = false;
        }

        private EventTrigger.Entry AddEntryevent(EventTriggerType _type, UnityAction _callback)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = _type;
            entry.callback = new EventTrigger.TriggerEvent();
            UnityAction<BaseEventData> callback = delegate (BaseEventData arg0)
            {
                _callback.Invoke();
            };
            entry.callback.AddListener(callback);
            return entry;
        }

        private void Updaterecordingprogress(float _progress)
        {
            recordprogress.fillAmount = _progress;
        }

        private void Onrecorded()
        {
            recordprogress.fillAmount = 0;
        }

        private void Onclickdropdown()
        {
            for (int i = 0; i < dropfunsprefab.Length; i++)
            {
                GameObject tmpsetup = Instantiate(dropfunsprefab[i], dropfunroot);
                tmpsetup.transform.localScale = Vector3.one;
                tmpsetup.transform.localPosition = Vector3.zero;
                tmpsetup.transform.localRotation = Quaternion.identity;
                dropfuns.Add(tmpsetup);
                if(dropfunsprefab[i].name.EndsWith("camera"))
                    tmpsetup.GetComponent<Button>().onClick.AddListener(()=> {Auxiliaryfun.Getinstance().Switchthecamera();});
                if (dropfunsprefab[i].name.EndsWith("flash"))
                    tmpsetup.GetComponent<Button>().onClick.AddListener(() => { Auxiliaryfun.Getinstance().Turnontheflash(true); });
                Debug.Log(i);
            }
            if (fade != null)
                fade.Kill(true);

            if (scaleY != null)
                scaleY.Kill(true);

            if (scaleall != null)
                scaleall.Kill(true);

            fade = droparea.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            scaleY = dropareacenter.transform.DOScaleY(1, 0.5f);
            //scaleall = droparea.transform.DOScale(Vector3.one, 0.5f);
            droparea.SetActive(true);
            dropdown.gameObject.SetActive(false);
            dropup.gameObject.SetActive(true);
        }

        private void Onclickdropup()
        {
            foreach (GameObject dropfun in dropfuns)
            {
                Destroy(dropfun);
            }
            if (fade != null)
                fade.Kill(true);

            if (scaleY != null)
                scaleY.Kill(true);

            if (scaleall != null)
                scaleall.Kill(true);

            fade = droparea.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
            scaleY = dropareacenter.transform.DOScaleY(0, 0.5f);
            scaleY.OnComplete(() => { /*scaleall = droparea.transform.DOScale(Vector3.zero, 0.4f)*/; droparea.SetActive(true); }); ;
            dropdown.gameObject.SetActive(true);
            dropup.gameObject.SetActive(false);
        }

    }
}
