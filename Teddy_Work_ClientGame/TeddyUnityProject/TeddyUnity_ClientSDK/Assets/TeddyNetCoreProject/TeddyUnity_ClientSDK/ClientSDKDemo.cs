using TeddyNetCore_EngineCore;
using UnityEngine;

public class ClientSDKDemo : MonoBehaviour {
    EngineBase engineBase;
    ClientSDK clientSDK;

    // Use this for initialization
    void Start () {
        engineBase = new EngineBase();
        clientSDK = new ClientSDK();
        clientSDK.init(engineBase);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
