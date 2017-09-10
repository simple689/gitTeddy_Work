using System.Collections;
using System.IO;
using UnityEngine;

namespace Teddy {
    public class UnitTestAssetBundle : MonoBehaviour, IUnitTest {
        public IEnumerator launch() {
            Log.instance.print("UnitTestAssetBundle");

            WWW www = new WWW("file:///" + Application.streamingAssetsPath + Path.DirectorySeparatorChar + "ResouceAssetBundle" + Path.DirectorySeparatorChar + "uiprefab.teddygame");
            yield return www;
            if (string.IsNullOrEmpty(www.error)) {
                var go = www.assetBundle.LoadAsset<GameObject>("UIHomePage");
                Instantiate(go);
            } else {
                Debug.LogError(www.error);
            }
            yield return null;
        }

        void Update() {
        }
    }
}