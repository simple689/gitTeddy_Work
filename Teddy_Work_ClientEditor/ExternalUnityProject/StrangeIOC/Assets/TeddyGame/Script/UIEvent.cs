using System.Collections.Generic;
using UnityEngine;

namespace Assets.TeddyGame {
    public enum UI_1 {
        Developing, // 开发版本,为了快速快发,而写的测试入口。
        Release // 发布版本,跑整个游戏
    }
    public enum UI_2 {
        Developing, // 开发版本,为了快速快发,而写的测试入口。
        Release // 发布版本,跑整个游戏
    }

    public class UIEvent : MonoBehaviour {
        //string a = "aa";
        //public Dictionary<UI_1, Dictionary<UI_2, string>> bb;
        //public UI_1 _ui_1 = UI_1.Developing;
        //public UI_2 _ui_2 = UI_2.Developing;
        public List<string> a;
    }

    //[System.Serializable]
    //public class CustomArrays {
    //    public float[] Array;
    //    public float this[int index] {
    //        get {
    //            return Array[index];
    //        }
    //    }
    //    public CustomArrays() {
    //        this.Array = new float[4];
    //    }
    //    public CustomArrays(int index) {
    //        this.Array = new float[index];
    //    }
    //}
}