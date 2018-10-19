using System;
using System.Globalization;

namespace Csv.Convert
{
    public static class CsvRowToObject<T> where T : new()
    {
        public static T Convert(CsvRow row)
        {
            var newObject = new T();

            foreach (var fieldValue in row.Fields) SetValue(fieldValue, newObject);

            return newObject;
        }

        private static object ConvertStringToPropertyType(Type type, string field, string value)
        {
            var trimmedValue = value.Trim();

            try
            {
                if (type == typeof(DateTime))
                    return string.IsNullOrEmpty(value) ? DateTime.MinValue : DateTime.Parse(trimmedValue);

                if (type == typeof(int) || type == typeof(decimal))
                {
                    trimmedValue = trimmedValue.Replace(" ", string.Empty);

                    if (type != typeof(decimal)) return int.Parse(trimmedValue);

                    try
                    {
                        return System.Convert.ToDecimal(trimmedValue, CultureInfo.CurrentCulture);
                    }
                    catch (FormatException)
                    {
                        return System.Convert.ToDecimal(trimmedValue, new CultureInfo("en-US"));
                    }
                }

                if (type == typeof(bool)) return bool.Parse(trimmedValue);

                return value;
            }
            catch (FormatException ex)
            {
                throw new ArgumentException($"Failed to parse \"{value}\" as {type.Name} for field {field}", ex);
            }
        }

        private static void SetValue(FieldValue fieldValue, object objectX)
        {
            if (objectX == null) throw new ArgumentNullException(nameof(objectX));
            var type = objectX.GetType();

            var prop = type.GetProperty(fieldValue.Name);

            if (prop == null) return;

            prop.SetValue(objectX, ConvertStringToPropertyType(prop.PropertyType, fieldValue.Name, fieldValue.Value),
                null);
        }
    }
}