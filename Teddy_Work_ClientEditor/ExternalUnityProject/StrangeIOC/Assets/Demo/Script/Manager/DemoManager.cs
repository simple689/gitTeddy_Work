using UnityEngine;
using TeddyFramwork;

namespace Game {
    public class DemoManager : IManager {
        public DemoManager() {
        }

        public void DoManagement() {
            Debug.Log("Manager implemented as a normal class");
        }
    }
}