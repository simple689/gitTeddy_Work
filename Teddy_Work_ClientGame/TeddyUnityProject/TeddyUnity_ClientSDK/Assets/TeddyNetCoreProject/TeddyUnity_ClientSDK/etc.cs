﻿
//        List<string> btnNameList = new List<string>();
//        btnNameList.Add("BtnPlay");
//        btnNameList.Add("BtnShop");
//        btnNameList.Add("BtnLeaderboards");

//        foreach (string btnName in btnNameList) {
//            GameObject btnObj = GameObject.Find(btnName);
//            Button btn = btnObj.GetComponent<Button>();
//            btn.onClick.AddListener(delegate () {
//                onClickEvent(btnObj);
//            });
//        }

//        Hashtable btnNameHashtable = new Hashtable();
//        btnNameHashtable.Add("BtnNewGame", "Button");
//        btnNameHashtable.Add("BtnContinue", "Button");
//        btnNameHashtable.Add("BtnChallenge", "Button");
//        btnNameHashtable.Add("BtnMoreGame", "Button");
//        btnNameHashtable.Add("BtnRank", "Button");
//        btnNameHashtable.Add("BtnMusic", "Toggle");
//        btnNameHashtable.Add("BtnSound", "Toggle");
//        foreach (DictionaryEntry btnInfo in btnNameHashtable) {
//            GameObject btnObj = GameObject.Find(btnInfo.Key as string);
//            if (btnInfo.Value as string == "Button") {
//                Button btn = btnObj.GetComponent<Button>();
//                btn.onClick.AddListener(delegate () {
//                    onClickEvent(btnObj);
//                });
//            } else if (btnInfo.Value as string == "Toggle") {
//                Toggle btn = btnObj.GetComponent<Toggle>();
//                btn.onValueChanged.AddListener(delegate (bool isOn) {
//                    //onValueChangedEvent(isOn, btnObj);
//                });
//            }
//        }

//        Button[] buttons = GetComponentsInChildren<Button>(true);
//        //GetComponent<Button>();
//        foreach (var item in buttons) {
//            Button btn = item;
//            btn.onClick.AddListener(delegate () {
//                //this.OnClickButtons(btn.gameObject);
//                //TestButtonClick testClick = GameObject.FindObjectOfType<TestButtonClick>();
//                //testClick.OnClickButton();
//            });
//        }

//        //Button btn = GameObject.Find("myBtn").GetComponent<Button>();
//        ////注册按钮的点击事件  
//        //btn.onClick.AddListener(delegate () {
//        //    //this.Btn_Test();
//        //});
//    }

//    //protected abstract void OnClickButtons(GameObject sender);

//    public void onClickEvent(GameObject sender) {
//        switch (sender.name) {
//            case "BtnPlay":
//            Debug.Log("BtnPlay");
//            break;
//            case "BtnShop":
//            Debug.Log("BtnShop");
//            break;
//            case "BtnLeaderboards":
//            Debug.Log("BtnLeaderboards");
//            break;
//            default:
//            Debug.Log("none");
//            break;
//        }
//    }
//}

//DataFile_PlotSection a = new DataFile_PlotSection();

//DataBase_PlotStep b = new DataBase_PlotStep();
//DataBase_PlotStep c = new DataBase_PlotStep();
//DataBase_PlotStep d = new DataBase_PlotStep();
//DataBase_PlotStep e = new DataBase_PlotStep();
//DataBase_PlotStep f = new DataBase_PlotStep();

//DataBase_Plot aa = new DataBase_Plot();
//DataBase_PlotUI bb = new DataBase_PlotUI();
//DataBase_PlotSound cc = new DataBase_PlotSound();

//a._plotStep._plotList.Add(aa);
//        a._plotStep._plotList.Add(bb);
//        a._plotStep._plotList.Add(cc);

//        a._plotStep._plotStepDict.Add("_plotStep_0", b);
//        a._plotStep._plotStepDict.Add("_plotStep_1", c);

//        b._plotStepDict.Add("_plotStep_0", d);
//        d._plotStepDict.Add("_plotStep_0", e);
//        e._plotStepDict.Add("_plotStep_0", f);

//        string str = _jsonController.serializeObject(a);
//        callBackLogPrint("--------------------------------------------------");
//        callBackLogPrint(str);
//        callBackLogPrint("--------------------------------------------------");