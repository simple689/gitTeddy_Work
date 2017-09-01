using System.Collections;
using UnityEngine;

namespace Teddy {
    public class UnitTestUI : MonoBehaviour, IUnitTest {
        public IEnumerator launch() {
            Log.instance.print("UnitTestUI");

            //ResourceManager.instance.Init();
            //ResourceManager.instance.LoadAssetBundle(UIPREFAB.BUNDLE_NAME);

            //UIManager.instance.SetResolution(1024, 768);
            //UIManager.instance.SetMatchOnWidthOrHeight(0);

            //UIManager.instance.OpenUI<UIHomePage>(UILevel.Common, UIPREFAB.BUNDLE_NAME);

            yield return null;
        }

        void Update() {
        }
    }
}