using UnityEngine;

namespace Teddy {
    public class FPSCounter { // 帧率计算器
        private const float _calcRate = 0.5f; // 帧率计算频率
        private int _frameCount = 0; // 本次计算频率下帧数
        private float _rateDuration = 0f; // 频率时长
        private int _fps = 0; // 显示帧率

        public FPSCounter(LogOutputConsole console) {
            console._onUpdate += onUpdate;
            console._onGUI += onGUI;
        }

        void start() {
            _frameCount = 0;
            _rateDuration = 0f;
            _fps = 0;
        }

        void onUpdate() {
            ++_frameCount;
            _rateDuration += Time.deltaTime;
            if (_rateDuration > _calcRate) {
                _fps = (int)(_frameCount / _rateDuration); // 计算帧率
                _frameCount = 0;
                _rateDuration = 0f;
            }
        }

        void onGUI() {
            GUI.color = Color.black;
            GUI.Label(new Rect(10, 50, 120, 20), "fps:" + _fps.ToString());
        }
    }
}