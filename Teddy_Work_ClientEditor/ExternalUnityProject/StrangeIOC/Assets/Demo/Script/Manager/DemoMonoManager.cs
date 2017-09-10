using UnityEngine;
using TeddyFramwork;

namespace Game {
    public class DemoMonoManager : MonoBehaviour, IManager {
        public void DoManagement() {
            Debug.Log("Manager implemented as MonoBehaviour");
        }
    }
}