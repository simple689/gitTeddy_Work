using UnityEngine;
using LitJson;
using System.Collections.Generic;

public class JsonArrayModel {
    public string panelTypeString;
    public string path;
}

public class JsonObjectModel {
    public List<JsonArrayModel> infoList;
}

public class ChangeScene : MonoBehaviour {
    public GameObject player;

    private GameObject terrain1 = null;
    private GameObject terrain2 = null;
    private GameObject terrain3 = null;

    void Start () {
        TextAsset jsonData = Resources.Load<TextAsset>("AssetExport_Map/JsonData");
        JsonObjectModel jsonObject = JsonMapper.ToObject<JsonObjectModel>(jsonData.text);
        foreach (var info in jsonObject.infoList) {
            Debug.Log(info.panelTypeString + "" + info.path);
        }
	}
	
	void Update () {
        Vector3 pos = player.transform.localPosition;
        //Debug.LogFormat("{0}, {1}", pos.x, pos.z);
        if (pos.x < 50 && pos.z < 50) {
            Debug.LogFormat("{0}", "1");
            if (!terrain1) {
                terrain1 = (GameObject)Instantiate(Resources.Load("AssetExport_Map/1_1_Main"));
                //GameObject mUICanvas = GameObject.Find("Canvas");
                //terrain1.transform.parent = mUICanvas.transform;
            }
            terrain1.transform.localPosition = new Vector3(-100, 0, 0);
            if (!terrain2) {
                terrain2 = (GameObject)Instantiate(Resources.Load("AssetExport_Map/2_1_Main"));
            }
            terrain2.transform.localPosition = new Vector3(0, 0, -100);
            if (!terrain3) {
                terrain3 = (GameObject)Instantiate(Resources.Load("AssetExport_Map/3_1_Main"));
            }
            terrain3.transform.localPosition = new Vector3(-100, 0, -100);
        } else if (pos.x < 50 && pos.z > 50) {
            Debug.LogFormat("{0}", "2");
        } else if (pos.x > 50 && pos.z < 50) {
            Debug.LogFormat("{0}", "3");
        } else if (pos.x > 50 && pos.z > 50) {
            Debug.LogFormat("{0}", "4");
        }
    }
}
