using APITest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RestSharp;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;
using System.Text.Unicode;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace APITesting
{
    [TestClass]
    public class UnitTest1 : AbstractApiTest
    {
        [Test]
        [TestCase( "200","users")]
        [TestCase("404", "usersx")]
        public async Task APIGetUsersAsync( string expectedStatusCode, string apiPath)
        {

            //arrange
            string[] expectedKeys = { "id", "name", "email", "gender", "status" };

            //act
            RestResponse response = await ExecuteRequest("Get", apiPath);
            //assert
            Assert.AreEqual(expectedStatusCode, ((int)response.StatusCode).ToString(), $"Status Code is not the Expected, Actual:{(int)response.StatusCode}");
            var answerArray = Newtonsoft.Json.JsonConvert.DeserializeObject<object[]>(response.Content);

            // Iterate through each object in the array and validate the keys
            foreach (var answer in answerArray)
            {
                var actualKeys = ((Newtonsoft.Json.Linq.JObject)answer).Properties().Select(p => p.Name).ToArray();
                Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert.AreEqual(expectedKeys, actualKeys, "Keys for each object are not the expected");
            }

        }
    }
}