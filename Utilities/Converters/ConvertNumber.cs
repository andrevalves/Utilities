namespace AndiSoft.Utilities.Converters
{
    /// <summary>
    /// Number Converter
    /// </summary>
    public static class ConvertNumber
    {
        ///<summary>
        ///Returns a float number. Returns 0 if the string is not a valid number.
        ///</summary>
        public static float ToFloat(string text)
        {
            try
            {
                return float.Parse(text.Replace('.', ','));
            } catch
            {
                return 0;
            }
        }

        ///<summary>
        ///Returns a double number. Returns 0 if the string is not a valid number.
        ///</summary>
        public static double ToDouble(string text)
        {
            try
            {
                return double.Parse(text.Replace('.', ','));
            } catch
            {
                return 0;
            }
        }

        ///<summary>
        ///Returns a decimal number. Returns 0 if the string is not a valid number.
        ///</summary>
        public static decimal ToDecimal(string text)
        {
            try
            {
                return decimal.Parse(text.Replace('.', ','));
            } catch
            {
                return 0;
            }
        }

        ///<summary>
        ///Returns a integer number. Returns 0 if the string is not a valid number.
        ///</summary>
        public static int ToInt(string text)
        {
            int.TryParse(text, out var number);
            return number;
        }
    }
}