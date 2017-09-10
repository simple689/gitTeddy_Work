using UnityEditor;
using UnityEngine;

public class 升序排列 : BaseHierarchySort { // 按字母升序排列 AscendingSort
    private readonly GUIContent _content;

    public 升序排列() {
        Texture2D image = Resources.Load<Texture2D>("IconSortAscending");
        if (image) {
            _content = new GUIContent("升序", image, "升序排列");
        } else {
            _content = new GUIContent("升序", "升序排列");
        }
    }

    public override GUIContent content {
        get { return _content; }
    }

    public override int Compare(GameObject lhs, GameObject rhs) {
        if (lhs == rhs) { return 0; }
        if (lhs == null) { return -1; }
        if (rhs == null) { return 1; }
        return EditorUtility.NaturalCompare(lhs.name, rhs.name);
    }
}