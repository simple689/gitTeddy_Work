using UnityEngine;
using UnityEditor;

public class UIToolWindow : EditorWindow {
    [MenuItem("TeddyTool/WorkUI/UI Tool", false, 11)]
    public static void show() {
        GetWindow<UIToolWindow>(false, "UI Tool", true).Show();
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