using Newtonsoft.Json.Linq;

namespace APITesting.Utilities
{
    public static class Utilities
    {
        /// <summary>
        /// Path to Json test
        /// </summary>
        public const string pathTestsJson = @"Tests/ApiTest/Jsons/";

        /// <summary>
        /// create random string with lenght
        /// </summary>
        /// <param name="length"> string lenght </param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Compare if Json x and Json y are Equal
        /// </summary>
        /// <param name="actualJson">Json Actual</param>
        /// <param name="expectedJson">Json Expected</param>
        /// <returns>Returns True if Json are Equals</returns>
        public static bool JsonEqualsTo(this string actualJson, string expectedJson)
        {
            return JToken.DeepEquals(JToken.Parse(expectedJson), JToken.Parse(actualJson));
        }

        /// <summary>
        /// Read Json from file jsonName
        /// </summary>
        /// <param name="jsonName">Json Name</param>
        /// <returns>string with Json  from the file</returns>
        public static string ReadJsonFromFile(string jsonName)
        => //Get json Data from file
          File.ReadAllText($"{pathTestsJson}{jsonName}");

    }
}

