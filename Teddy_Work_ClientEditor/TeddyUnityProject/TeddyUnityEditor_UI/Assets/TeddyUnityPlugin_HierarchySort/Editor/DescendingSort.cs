using UnityEditor;
using UnityEngine;

public class 降序排列 : BaseHierarchySort { // 按字母降序排列 DescendingSort
    private readonly GUIContent _content;

    public 降序排列() {
        Texture2D image = Resources.Load<Texture2D>("IconSortDescending");
        if (image) {
            _content = new GUIContent("降序", image, "降序排列");
        } else {
            _content = new GUIContent("降序", "降序排列");
        }
    }

    public override GUIContent content {
        get { return _content; }
    }

    public override int Compare(GameObject lhs, GameObject rhs) {
        if (lhs == rhs) { return 0; }
        if (lhs == null) { return 1; }
        if (rhs == null) { return -1; }
        return EditorUtility.NaturalCompare(rhs.name, lhs.name);
    }
}