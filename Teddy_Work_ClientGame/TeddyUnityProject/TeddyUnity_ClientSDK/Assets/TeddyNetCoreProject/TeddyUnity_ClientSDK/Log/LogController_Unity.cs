using System;
using TeddyNetCore_EngineCore;
using UnityEngine;

public class LogController_Unity : LogController {
    public override void println(string str) {
        try {
            Debug.Log(str);
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
