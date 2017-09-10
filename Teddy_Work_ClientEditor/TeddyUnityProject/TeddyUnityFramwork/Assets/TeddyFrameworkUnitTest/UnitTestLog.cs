using System.Collections;
using UnityEngine;

namespace Teddy {
    public class UnitTestLog : MonoBehaviour, IUnitTest {
        public IEnumerator launch() {
            Log.instance.print("UnitTestLog");
            yield return null;
        }

        void Update() {
        }
    }
}