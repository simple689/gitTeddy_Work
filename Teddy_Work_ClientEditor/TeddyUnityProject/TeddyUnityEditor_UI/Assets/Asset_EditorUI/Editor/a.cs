using UnityEngine;
using UnityEditor;

namespace Teddy {
    public class a : EditorWindow {
        int _id = 0;
        GameObject _obj;
        int index = 0;
        string[] options = new string[] { "Cube", "Sphere", "Plane"};

        [MenuItem("TeddyTool/UI Window", false, 1)]
        public static void show() {
            GetWindow<a>(false, "MyWindow", true).Show();
        }
        void OnEnable() {
            Selection.selectionChanged += ccc;
            
            //switch (EditorUserBuildSettings.activeBuildTarget) {
            //    case BuildTarget.StandaloneWindows: {
            //        _buildPlatformIndex = 0;
            //        break;
            //    }
            //    case BuildTarget.iOS: {
            //        _buildPlatformIndex = 1;
            //        break;
            //    }
            //    case BuildTarget.Android: {
            //        _buildPlatformIndex = 2;
            //        break;
            //    }
            //    default: {
            //        _buildPlatformIndex = 0;
            //        break;
            //    }
            //}
        }

        void ccc() {
            Repaint();
        }

        void drawInfo() {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name:");
            GUILayout.Label(_obj.name);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("ID:");
            GUILayout.Label(_obj.GetInstanceID().ToString());
            GUILayout.EndHorizontal();
        }

        void draw() {
            //_obj.AddComponent<BBBBB>();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Script:");
            //GUILayout.Label(_obj.GetComponent<BBBBB>().GetType().ToString());
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("一级目录:");
            index = EditorGUILayout.Popup(index, options);
            //index = EditorGUILayout.Popup(index, options);
            if (GUILayout.Button("Create"))
                InstantiatePrimitive();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("二级目录:");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("三级目录:");
            GUILayout.EndHorizontal();
        }

        void InstantiatePrimitive() {
            switch (index) {
                case 0:
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = Vector3.zero;
                break;
                case 1:
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = Vector3.zero;
                break;
                case 2:
                GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                plane.transform.position = Vector3.zero;
                break;
                default:
                Debug.LogError("Unrecognized Option");
                break;
            }
        }
        void OnGUI() {
            _obj = Selection.activeGameObject;
            //Debug.Log("Selected Transform is on " + Selection.activeObject.name + ".");
            //foreach(string guid in Selection.assetGUIDs){
            //    Debug.Log("GUID " + guid);
            //}

            GUILayout.BeginVertical();
            drawInfo();
            draw();
            GUILayout.EndVertical();

            //_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Width(500), GUILayout.Height(400));

            //GUILayout.TextField(Application.persistentDataPath);

            //if (GUILayout.Button("Go To Persistance")) {
            //    EditorUtility.RevealInFinder(Application.persistentDataPath);
            //}
            //_isEnableGenerateClass = GUILayout.Toggle(_isEnableGenerateClass, "auto generate class");

            //GUILayout.BeginHorizontal();
            //GUILayout.Label("ResVersion:");
            //_resVersion = GUILayout.TextField(_resVersion);
            //GUILayout.EndHorizontal();

            //GUILayout.BeginHorizontal();
            //GUILayout.Label("Project Tag:");
            //_projectTag = GUILayout.TextField(_projectTag);
            ////isUseFramework = GUILayout.Toggle (isUseFramework, "zip the framework");
            //GUILayout.EndHorizontal();

            //if (GUILayout.Button("Build")) {
            //}
            //if (GUILayout.Button("ForceClear")) {
            //}
            //GUILayout.Space(50);
            //EditorGUILayout.EndScrollView();

        }
    }
}


