using System.Collections;
using TeddyNetCore_EngineCore;
using UnityEngine;
using UnityEngine.UI;

public class ClientSDKDemo : MonoBehaviour {
    ClientSDK clientSDK;

    // Use this for initialization
    void Start () {
        Debug.Log("/* 游戏开启 ClientSDKDemo */");
        clientSDK = new ClientSDK();
        clientSDK.init(new EngineBase(), "/Assets/Product_ClientSDK");
        clientSDK.start();

        initClickEvent();
    }

    // Update is called once per frame
    void Update () {
        clientSDK.update();
    }

    void OnGui() {

    }

    void Reset() {

    }

    void OnDisable() {

    }

    void OnDestroy() {
        clientSDK.stop();
    }

    void initClickEvent() {
        Hashtable btnNameHashtable = new Hashtable();
        btnNameHashtable.Add("BtnNewGame", "Button");
        btnNameHashtable.Add("BtnContinue", "Button");
        btnNameHashtable.Add("BtnChallenge", "Button");
        btnNameHashtable.Add("BtnMoreGame", "Button");
        btnNameHashtable.Add("BtnRank", "Button");
        btnNameHashtable.Add("BtnMusic", "Toggle");
        btnNameHashtable.Add("BtnSound", "Toggle");
        foreach (DictionaryEntry btnInfo in btnNameHashtable) {
            GameObject btnObj = GameObject.Find(btnInfo.Key as string);
            if (btnInfo.Value as string == "Button") {
                Button btn = btnObj.GetComponent<Button>();
                btn.onClick.AddListener(delegate () {
                    onClickEvent(btnObj);
                });
            } else if (btnInfo.Value as string == "Toggle") {
                Toggle btn = btnObj.GetComponent<Toggle>();
                btn.onValueChanged.AddListener(delegate (bool isOn) {
                    //onValueChangedEvent(isOn, btnObj);
                });
            }
        }
    }

    //protected abstract void OnClickButtons(GameObject sender);

    public void onClickEvent(GameObject sender) {
        switch (sender.name) {
            case "BtnPlay":
            Debug.Log("BtnPlay");
            break;
            case "BtnShop":
            Debug.Log("BtnShop");
            break;
            case "BtnLeaderboards":
            Debug.Log("BtnLeaderboards");
            break;
            default:
            Debug.Log("none");
            break;
        }
    }
}
