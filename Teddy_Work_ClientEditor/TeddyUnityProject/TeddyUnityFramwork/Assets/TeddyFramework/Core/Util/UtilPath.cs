using UnityEngine;

namespace Teddy {
    public class UtilPath { // 所有的路径常量都在这里
        public const string _personalPath = "/PersonalPath/";
        public const string _abRelativePath = "/ResouceAssetBundle/"; // 相对资源路径.

        public static string abBuildOutPutDir(RuntimePlatform platform) { // 资源输出的路径
            string dir = null;
            switch (platform) {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor: {
                    dir = Application.streamingAssetsPath + _abRelativePath + "Windows";
                    break;
                }
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.OSXEditor: {
                    dir = Application.streamingAssetsPath + _abRelativePath + "OSX";
                    break;
                }
                case RuntimePlatform.IPhonePlayer: {
                    dir = Application.streamingAssetsPath + _abRelativePath + "IOS";
                    break;
                }
                case RuntimePlatform.Android: {
                    dir = Application.streamingAssetsPath + _abRelativePath + "Android";
                    break;
                }
            }
            return dir;
        }

        //public static string dataPath { // 取得数据存放目录
        //    get {
        //        string game = Path.RelativeABPath.ToLower();
        //        if (Application.isMobilePlatform) {
        //            return Application.persistentDataPath + "/" + game + "/";
        //        }
        //        if (AppConst.DebugMode) {
        //            return Application.streamingAssetsPath;
        //        }
        //        if (Application.platform == RuntimePlatform.OSXEditor) {
        //            int i = Application.dataPath.LastIndexOf('/');
        //            return Application.dataPath.Substring(0, i + 1) + game + "/";
        //        }
        //        return "c:/" + game + "/";
        //    }
        //}

        //public static string FrameworkPath {
        //    get {
        //        return Application.dataPath + "/" + Path.RelativeABPath;
        //    }
        //}
    }
}
