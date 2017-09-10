using UnityEngine;
using TeddyFramwork;

namespace TeddyGame {
    public class EnemyManager : MonoBehaviour, IManager {
        public void DoManagement() {
            Debug.Log("Manager implemented as a normal class");
        }
    }
}