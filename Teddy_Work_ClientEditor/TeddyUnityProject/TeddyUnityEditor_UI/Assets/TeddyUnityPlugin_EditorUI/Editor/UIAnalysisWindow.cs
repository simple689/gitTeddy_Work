using UnityEngine;
using UnityEditor;

public class UIAnalysisWindow : EditorWindow {
    [MenuItem("TeddyTool/WorkUI/UI Analysis", false, 11)]
    public static void show() {
        UIAnalysisWindow window = (UIAnalysisWindow)GetWindow<UIAnalysisWindow>(false, "UI Analysis", true);
        window.analysis();
        window.Show();
    }

    void analysis() {
        string[] allGuid = AssetDatabase.FindAssets("t:Scene", new string[] { "Assets" });
        foreach (string guid in allGuid) {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
        }
    }

    void OnEnable() {
        //Selection.selectionChanged += Repaint;
    }

    void draw() {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Script:");
        GUILayout.EndHorizontal();
    }

    void OnGUI() {
        GUILayout.BeginVertical();
        draw();
        GUILayout.EndVertical();
    }
}