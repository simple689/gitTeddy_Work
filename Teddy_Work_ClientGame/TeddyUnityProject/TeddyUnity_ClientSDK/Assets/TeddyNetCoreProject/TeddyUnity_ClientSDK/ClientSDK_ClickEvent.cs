using UnityEngine;
using UnityEngine.UI;

public class ClientSDK_ClickEvent {
    //2.定义一个目标对象。  
    public Text targetTextObject;

    //3.声明一个公开的点击按钮的事件方法。  
    public void login() {
        targetTextObject.text = "点击了按钮！";
    }
}
