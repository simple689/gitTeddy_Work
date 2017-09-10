namespace TeddyNetCore_EngineCore {
    public static class Util {
        public static bool isValidString(string str) {
            if (str.Length > 0 && str != "None") {
                return true;
            }
            return false;
        }
    }
}
