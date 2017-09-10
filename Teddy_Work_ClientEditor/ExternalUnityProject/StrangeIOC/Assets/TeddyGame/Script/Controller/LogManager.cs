using UnityEngine;
using TeddyFramwork;

namespace TeddyGame {
    public class LogManager : IManager {
        public LogManager() { }

        public void DoManagement() {
            Debug.Log("LogManager");
        }
    }
}