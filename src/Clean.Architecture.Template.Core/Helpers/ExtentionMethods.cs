namespace Clean.Architecture.Template.Core.Helpers
{
    public static class ExtentionMethods
    {
        public static string FirstCharToUpper(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1)),
            };
        }

        public static string ToCommaSeparatedList(this List<int> items)
        {
            return items == null || items.Count == 0
                ? string.Empty
                : string.Join(",", items);
        }

        public static string ToCommaSeparatedList(this List<string> items)
        {
            return items == null || items.Count == 0
                ? string.Empty
                : string.Join(",", items);
        }

        public static int[] GetIntArrayFromCommaSeparatedString(this string str)
        {
            return string.IsNullOrEmpty(str)
                ? []
                : str.Split(',').Select(int.Parse).ToArray();
        }

        /// <summary>
        /// Validates int for dynamic queries.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsNotNullZero(this int? num)
        {
            return (num != 0 && num != null);
        }
    }
}