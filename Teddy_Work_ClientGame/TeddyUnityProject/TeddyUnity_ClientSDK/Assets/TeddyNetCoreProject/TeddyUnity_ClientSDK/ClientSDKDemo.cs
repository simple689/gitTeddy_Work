using TeddyNetCore_EngineCore;
using UnityEngine;

public class ClientSDKDemo : MonoBehaviour {
    ClientSDK clientSDK;

    // Use this for initialization
    void Start () {
        Debug.Log("/* 游戏开启 ClientSDKDemo */");
        clientSDK = new ClientSDK();
        clientSDK.init(new EngineBase());
        clientSDK.start();
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

    public void clickEvent(GameObject sender) {
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
