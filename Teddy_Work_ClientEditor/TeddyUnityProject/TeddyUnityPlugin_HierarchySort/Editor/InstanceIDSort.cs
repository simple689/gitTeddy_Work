using UnityEditor;
using UnityEngine;

namespace TeddyUnityPlugin_HierarchySort {
    public class InstanceID排序 : BaseHierarchySort { // 按InstanceID排序 InstanceIDSort
        private readonly GUIContent _content;

        public InstanceID排序() {
            Texture2D image = Resources.Load<Texture2D>("IconSortInstanceID");
            if (image) {
                _content = new GUIContent("InstanceID", image, "InstanceID排序");
            } else {
                _content = new GUIContent("InstanceID", "InstanceID排序");
            }
        }

        public override GUIContent content {
            get { return _content; }
        }

        public override int Compare(GameObject lhs, GameObject rhs) {
            if (lhs == rhs) { return 0; }
            if (lhs == null) { return -1; }
            if (rhs == null) { return 1; }
            return lhs.GetInstanceID().CompareTo(rhs.GetInstanceID());
        }
    }
}