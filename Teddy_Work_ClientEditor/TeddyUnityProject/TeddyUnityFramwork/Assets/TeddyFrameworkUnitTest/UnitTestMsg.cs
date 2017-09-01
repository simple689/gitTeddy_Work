using System.Collections;
using UnityEngine;

namespace Teddy {
    public class UnitTestMsg : MonoBehaviour, IUnitTest, IMsgReceiver, IMsgSender {
        public const string _receiveMsgFromOtherObject = "ReceiveMsgFromOtherObject";
        public bool _isShowButton = true;

        public IEnumerator launch() {
            Log.instance.print("UnitTestMsg");

            MsgDispatcher.instance.registerMsgGlobal(this, _receiveMsgFromOtherObject, delegate (object[] paramList) { // 接收消息，需要实现IMsgReceiver接口
                Log.instance.print("registerMsgGlobal");
                int index = -1;
                foreach (object msgContentItem in paramList) {
                    Debug.Log(msgContentItem);
                    //Log.instance.print(msgContentItem);
                    index++;
                    if (index == 0) {
                        UnitTestMsg obj = (UnitTestMsg)msgContentItem;
                        //obj._isShowButton = false;
                    }
                }
            });

            MsgDispatcher.instance.registerMsgByChannel(this, MsgChannel.UI, _receiveMsgFromOtherObject, delegate (object[] paramList) {
                Log.instance.print("这里接收不到消息,因为通道不一样");
            });

            MsgDispatcher.instance.registerMsgByChannel(this, MsgChannel.Logic, _receiveMsgFromOtherObject, receiverMsgLogic);
            yield return null;
        }

        void OnGUI() {
            if (_isShowButton && GUI.Button(new Rect(0, 0, 100, 50), "Send Msg")) {
                //MsgDispatcher.instance.sendMsgGlobal(this, _receiveMsgFromOtherObject, new object[] { "1", "2", 123 }); // 发送消息，需要实现IMsgSender接口
                MsgDispatcher.instance.sendMsgGlobal(this, _receiveMsgFromOtherObject, new object[] { this, "1", "2", 123 }); // 发送消息，需要实现IMsgSender接口
                MsgDispatcher.instance.sendMsgByChannel(this, MsgChannel.UI, _receiveMsgFromOtherObject, new object[] { "1", "2", 123 }); // 发送消息，需要实现IMsgSender接口
            }
        }

        void OnDestroy() {
            MsgDispatcher.instance.unRegisterMsgGlobal(this, _receiveMsgFromOtherObject);
            MsgDispatcher.instance.unRegisterMsgByChannel(this, MsgChannel.UI, _receiveMsgFromOtherObject);
        }

        void receiverMsgLogic(params object[] paramList) {
            foreach (var msgContentItem in paramList) {
                Log.instance.print(msgContentItem.ToString());
            }
        }
    }
}