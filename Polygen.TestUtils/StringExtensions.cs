namespace Polygen.TestUtils
{
    /// <summary>
    /// Unit test helper extension methods for String class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Trims the string from leading and following whitespace, converts \r\n to \n and replaces ' with "
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterXmlString(this string str)
        {
            return str.Trim().Replace("\r\n", "\n").Replace("'", "\"");
        }
    }
}