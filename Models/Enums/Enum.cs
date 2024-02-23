using System.ComponentModel;
using System.Reflection;

namespace auction.Models.Enums
{
    public class EnumUtils
    {
        public enum UserRole
        {
            Admin,
            User,
            // Add other roles as needed
        }

        public static string StringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }

        public enum FileTypeBase64
        {
            [Description("data:image/jpeg;base64,")]
            jpeg,
            [Description("data:image/png;base64,")]
            png,
            [Description("data:image/jpeg;base64,")]
            jpg,
            [Description("data:image/webp;base64,")]
            webp,
        }
    }
}
