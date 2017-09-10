using UnityEngine;
using System.Collections.Generic;

namespace Teddy {
    public class LogOutputConsole : ILogOutput { // 控制台GUI输出类，包括FPS，内存使用情况，日志GUI输出
        public delegate void OnUpdateCallback();
        public delegate void OnGUICallback();
        public OnUpdateCallback _onUpdate = null;
        public OnGUICallback _onGUI = null;

        private FPSCounter _fpsCounter = null; // FPS计数器
        private MemoryDetector _memoryDetector = null; // 内存监视器

        private bool _isShowGUI = false;
        bool _isTouching = false;
        List<LogData> _logDataList = new List<LogData>();

        const int _margin = 20;
        Rect _windowRect = new Rect(_margin + Screen.width * 0.5f, _margin, Screen.width * 0.5f - (2 * _margin), Screen.height - (2 * _margin));
        bool _isScrollToBottom = true;
        Vector2 _scrollPos;
        bool _collapse;

        GUIContent _clearLabel = new GUIContent("Clear", "Clear the contents of the console.");
        GUIContent _collapseLabel = new GUIContent("Collapse", "Hide repeated messages.");
        GUIContent _scrollToBottomLabel = new GUIContent("ScrollToBottom", "Scroll bar always at bottom");

        public LogOutputConsole() {
            _fpsCounter = new FPSCounter(this);
            _memoryDetector = new MemoryDetector(this);
            _isTouching = false;

            App.instance._onUpdate += onUpdate;
            App.instance._onGUI += onGUI;
        }

        void onUpdate() {
#if UNITY_EDITOR
            if (Input.GetKeyUp(KeyCode.F1)) {
                _isShowGUI = !_isShowGUI;
            }
#elif UNITY_ANDROID
			if (Input.GetKeyUp(KeyCode.Escape)) {
				_isShowGUI = !_isShowGUI;
            }
#elif UNITY_IOS
			if (!_isTouching && Input.touchCount == 4) {
				_isTouching = true;
				_isShowGUI = !_isShowGUI;
			} else if (Input.touchCount == 0){
				_isTouching = false;
			}
#endif
            if (_onUpdate != null) {
                _onUpdate();
            }
        }

        void onGUI() {
            if (!_isShowGUI) {
                return;
            }
            if (_onGUI != null) {
                _onGUI();
            }
//            if (GUI.Button(new Rect(100, 100, 200, 100), "清空数据")) {
//                PlayerPrefs.DeleteAll();
//#if UNITY_EDITOR
//                //EditorApplication.isPlaying = false; // hh
//#else
//				Application.Quit();
//#endif
//            }
            _windowRect = GUILayout.Window(123456, _windowRect, ConsoleWindow, "Console");
        }

        public void print(LogData logData) {
            _logDataList.Add(logData);
        }

        public void close() {
        }

        void ConsoleWindow(int windowID) { // A window displaying the logged messages.
            if (_isScrollToBottom) {
                GUILayout.BeginScrollView(Vector2.up * _logDataList.Count * 100.0f);
            } else {
                _scrollPos = GUILayout.BeginScrollView(_scrollPos);
            }
            for (int i = 0; i < _logDataList.Count; i++) { // Go through each logged entry
                LogData logData = _logDataList[i];
                if (_collapse && i > 0 && logData._log == _logDataList[i - 1]._log) { // If this message is the same as the last one and the collapse feature is chosen, skip it
                    continue;
                }
                switch (logData._level) { // Change the text colour according to the log type
                    case LogLevel.Error:
                    case LogLevel.Exception: {
                        GUI.contentColor = Color.red;
                        break;
                    }
                    case LogLevel.Warning: {
                        GUI.contentColor = Color.yellow;
                        break;
                    }
                    default: {
                        GUI.contentColor = Color.white;
                        break;
                    }
                }
                if (logData._level == LogLevel.Exception || logData._level == LogLevel.Error) {
                    GUILayout.Label(logData._log + " || " + logData._stackTrace);
                } else {
                    GUILayout.Label(logData._log);
                }
            }
            GUI.contentColor = Color.white;
            GUILayout.EndScrollView();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_clearLabel)) { // Clear button
                _logDataList.Clear();
            }
            _collapse = GUILayout.Toggle(_collapse, _collapseLabel, GUILayout.ExpandWidth(false)); // Collapse toggle
            _isScrollToBottom = GUILayout.Toggle(_isScrollToBottom, _scrollToBottomLabel, GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
            GUI.DragWindow(new Rect(0, 0, 10000, 20)); // Set the window to be draggable by the top title bar
        }
    }
}