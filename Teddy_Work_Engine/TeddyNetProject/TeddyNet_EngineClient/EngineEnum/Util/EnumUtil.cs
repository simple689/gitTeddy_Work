using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TeddyNetCore_EngineEnum {
    public static class EnumUtil<T> {

        public static string getDisplayValue(T value) {
            string result = "";
            var field = value.GetType().GetField(value.ToString());
            var display = (DisplayAttribute[])field.GetCustomAttributes(typeof(DisplayAttribute), false);
            result = (display.Length > 0) ? display[0].Name : value.ToString();
            return result;
        }

        //public static IList<T> getValues(Enum value) {
        //    var enumValues = new List<T>();
        //    foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public)) {
        //        enumValues.Add((T)Enum.Parse(value.GetType(), fi.Name, false));
        //    }
        //    return enumValues;
        //}

        //public static T parse(string value) {
        //    return (T)Enum.Parse(typeof(T), value, true);
        //}

        //public static IList<string> getNames(Enum value) {
        //    return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        //}

        //public static IList<string> getDisplayValues(Enum value) {
        //    return getNames(value).Select(obj => getDisplayValue(parse(obj))).ToList();
        //}
    }
}
