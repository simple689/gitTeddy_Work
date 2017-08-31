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
        Button[] btns = GetComponents<Button>();
        foreach (Button btn in btns) {
            btn.onClick.AddListener(delegate () {
                onClickEvent(btn.gameObject);
            });
        }
        Toggle[] toggles = GetComponents<Toggle>();
        foreach (Toggle toggle in toggles) {
            toggle.onValueChanged.AddListener(delegate (bool isOn) {
                //            //onValueChangedEvent(isOn, toggle.gameObject);
            });
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
