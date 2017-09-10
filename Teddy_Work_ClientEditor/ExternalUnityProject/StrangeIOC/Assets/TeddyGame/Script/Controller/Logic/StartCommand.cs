using UnityEngine;
using strange.extensions.command.impl;

namespace TeddyGame {
    public class StartCommand : Command {
        public override void Execute() {
            // perform all game start setup here
            Debug.Log("StartCommand");
        }
    }
}