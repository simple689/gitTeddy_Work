namespace TeddyNetCore_EngineEnum {
    public static class EnumUtil<T> {

        public static string getDisplayValue(T value) {
            string result = "";
            var field = value.GetType().GetField(value.ToString());
            var display = (DisplayAttribute[])field.GetCustomAttributes(typeof(DisplayAttribute), false);
            result = (display.Length > 0) ? display[0].Name : value.ToString();
            return result;
        }
    }
}
