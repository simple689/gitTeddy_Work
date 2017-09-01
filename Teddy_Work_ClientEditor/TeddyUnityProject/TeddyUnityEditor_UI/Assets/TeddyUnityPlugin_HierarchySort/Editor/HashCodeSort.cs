using UnityEditor;
using UnityEngine;

public class 哈希表排列 : BaseHierarchySort { // 按哈希表排序 HashCodeSort
    private readonly GUIContent _content;

    public 哈希表排列() {
        Texture2D image = Resources.Load<Texture2D>("IconSortHashCode");
        if (image) {
            _content = new GUIContent("哈希表", image, "哈希表排列");
        } else {
            _content = new GUIContent("哈希表", "哈希表排列");
        }
    }

    public override GUIContent content {
        get { return _content; }
    }

    public override int Compare(GameObject lhs, GameObject rhs) {
        if (lhs == rhs) { return 0; }
        if (lhs == null) { return -1; }
        if (rhs == null) { return 1; }
        return lhs.GetHashCode().CompareTo(rhs.GetHashCode());
    }
}