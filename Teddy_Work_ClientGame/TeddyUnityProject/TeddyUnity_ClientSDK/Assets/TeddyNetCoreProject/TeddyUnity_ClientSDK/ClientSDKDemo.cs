using System.Collections;
using TeddyNetCore_EngineCore;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClientSDKDemo : MonoBehaviour {
    ClientSDK clientSDK;

    void Start () {
        Debug.Log("/* 游戏开启 ClientSDKDemo */");
        clientSDK = new ClientSDK();
        clientSDK.init(new EngineBase(), "/Assets/Product_ClientSDK");
        clientSDK.start();

        initClickEvent();
    }

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
        GameObject[] objs = SceneManager.GetActiveScene().GetRootGameObjects();
        Button[] btns = GetComponents<Button>();
        foreach (Button btn in btns) {
            UnityAction call = delegate () {
                onClickEvent(btn.gameObject);
            };
            btn.onClick.AddListener(call);
        }
    }

    //protected abstract void OnClickButtons(GameObject sender);

    public void onClickEvent(GameObject sender) {
        Debug.Log("onClickEvent");
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
