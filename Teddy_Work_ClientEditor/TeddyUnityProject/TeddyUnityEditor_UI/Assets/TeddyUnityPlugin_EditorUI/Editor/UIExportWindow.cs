using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class UIExportWindow : EditorWindow {
    Vector2 _scroll = Vector2.zero;
    public Dictionary<string, BetterList<string>> _dict = new Dictionary<string, BetterList<string>>();
    bool _groupEnabledScene = true;

    [MenuItem("TeddyTool/WorkUI/UI Export", false, 11)]
    public static void show() {
        UIExportWindow window = (UIExportWindow)GetWindow<UIExportWindow>(false, "UI Export", true);
        window.prepare();
        window.Show();
    }

    public void showProgress(float val, int total, int cur) {
        EditorUtility.DisplayProgressBar("Searching", string.Format("Finding ({0}/{1}), please wait...", cur, total), val);
    }

    public void clearProgress() {
        EditorUtility.ClearProgressBar();
    }

    void prepare() {
        showProgress(0, 0, 0);
        string[] allGuid = AssetDatabase.FindAssets("t:Scene", new string[] { "Assets" });
        BetterList<string> sceneList = new BetterList<string>();

        int i = 0;
        foreach (string guid in allGuid) {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (assetPath.EndsWith(".unity")) {
                sceneList.Add(assetPath);
            }
            showProgress((float)i / (float)allGuid.Length, allGuid.Length, i);
            i++;
        }
        if (_dict != null) {
            _dict.Clear();
        }
        _dict.Add("scene", sceneList);
        clearProgress();
    }

    void loadScene() {
        BetterList<string> list = _dict["scene"];
        if (list != null && list.size > 0) {
            foreach (string item in list) {
                Scene scene = EditorSceneManager.OpenScene(item);
                if (scene == null) {
                    continue;
                }
                GameObject[] sceneObjs = scene.GetRootGameObjects();
                foreach (GameObject sceneObj in sceneObjs) {
                    GameObject obj = GameObject.Find(scene.name);
                    if (obj) {
                        string savePath = "Assets/";
                        savePath += obj.name;
                        savePath += ".prefab";
                        PrefabUtility.CreatePrefab(savePath, obj);
                    }
                }
            }
            list = null;
        }
    }

    void Callback(object obj) {
        Debug.Log("Selected: " + obj);
        prepare();
    }

    void drawGenericMenu() {
        Event currentEvent = Event.current;
        if (currentEvent.type == EventType.ContextClick) {
            Vector2 mousePos = currentEvent.mousePosition;
            Rect windowRect = this.position;
            windowRect.x = windowRect.y = 0;
            if (windowRect.Contains(mousePos)) {
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("MenuItem1"), false, Callback, "item 1");
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("UI/MenuItem3"), false, Callback, "item 3");
                menu.ShowAsContext();
                currentEvent.Use();
            }
        }
    }

    void OnEnable() {
        //Selection.selectionChanged += Repaint;
    }

    void OnDisable() {
    }

    void OnGUI() {
        if (_dict == null) {
            return;
        }
        drawGenericMenu();

        _scroll = GUILayout.BeginScrollView(_scroll);

        BetterList<string> list = _dict["scene"];
        _groupEnabledScene = EditorGUILayout.BeginToggleGroup("scene", _groupEnabledScene);
        int i = 0;
        if (list != null && list.size > 0) {
            if (NGUIEditorTools.DrawHeader("scene")) {
                foreach (string item in list) {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(20));
                    EditorGUILayout.Toggle(true, GUILayout.Width(20));
                    EditorGUILayout.LabelField(item);
                    EditorGUILayout.EndHorizontal();
                    i++;
                }
            }
            list = null;
        }
        EditorGUILayout.EndToggleGroup();

        GUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Repaint"))
            Repaint();
        if (GUILayout.Button("LoadScene"))
            loadScene();
        EditorGUILayout.EndHorizontal();
    }
}