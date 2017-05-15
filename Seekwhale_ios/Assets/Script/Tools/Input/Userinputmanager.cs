namespace SeekWhale
{
    /*
	* 功 能： N/A
	* 类 名： Baseinput
	* Email: paris3@163.com
	* 作 者： NSWell
	* Copyright (c) SeekWhale. All rights reserved.
	*/

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Userinputmanager : Singleton<Userinputmanager>
    {
        public Inputtype inputtype;
        
        public Physicsinputmoduloption option;
        private Physicsinput physicsinput = null;

        private void Start()
        {
            SetupModul();
        }

        public void SetupModul()
        {
            switch (inputtype)
            {
                case Inputtype.PHYSICS:
                    physicsinput = new Physicsinput();
                    physicsinput.Modulinit(option);
                    break;
                case Inputtype.AXIS:
                    //realse physics input
                    if (physicsinput != null)
                    {
                        physicsinput.Onrelease();
                        physicsinput = null;
                        option = null;
                    }
                    
                    break;
            }
        }


        public void Update()
        {

            switch (inputtype)
            {
                case Inputtype.PHYSICS:
                    if (!Input.GetMouseButtonDown(0)) break;                    
                    if (physicsinput == null || option == null) break;
                    physicsinput.Whenuserclicktoretuanresult(out option.result);                    
                    if (option.result) Debug.Log(option.result.name);
                    option.Ontriggerevent();
                    break;
                case Inputtype.AXIS:
                    break;
            }
        }
    }
}
