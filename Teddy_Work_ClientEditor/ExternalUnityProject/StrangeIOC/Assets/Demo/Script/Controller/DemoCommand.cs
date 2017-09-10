using UnityEngine;
using strange.extensions.command.impl;

namespace Game {
    public class DemoCommand : Command {
        public override void Execute() {
            // perform all game start setup here
            Debug.Log("Hello World");
        }
    }
}