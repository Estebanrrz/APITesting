using APITest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RestSharp;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;
using System.Text.Unicode;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Xml.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace APITesting
{
    [TestClass]
    public class ApiTests : AbstractApiTest
    {
        [Test]
        [TestCase("200", "users")]
        [TestCase("404", "usersx")]
        public async Task APIGetUsersAsync(string expectedStatusCode, string apiPath)
        {

            //arrange
            string[] expectedKeys = { "id", "name", "email", "gender", "status" };

            //act
            RestResponse response = await ExecuteRequest("Get", apiPath);
            //assert
            Assert.AreEqual(expectedStatusCode, ((int)response.StatusCode).ToString(), $"Status Code is not the Expected, Actual:{(int)response.StatusCode}");
            
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var answerArray = Newtonsoft.Json.JsonConvert.DeserializeObject<object[]>(response.Content);

                // Assert that the response contains the expected number of objects
                Assert.AreEqual(10, answerArray.Length, "Number of objects in the response is not the expected");

                // Iterate through each object in the array and validate the keys
                foreach (var answer in answerArray)
                {
                    var actualKeys = ((Newtonsoft.Json.Linq.JObject)answer).Properties().Select(p => p.Name).ToArray();
                    Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert.AreEqual(expectedKeys, actualKeys, "Keys for each object are not the expected");
                }
            }
        }

        [Test]
        [TestCase("200", "name", "Chandraswaroopa Dwivedi II")]
        [TestCase("200", "id", "1831483")]
        [TestCase("200", "email", "dwivedi_ii_chandraswaroopa@kuhlman-koss.test")]
        public async Task APIGetUserByAsync(string expectedStatusCode, string parameter, string value)
        {

            //arrange
          
            string path= value;
            string encodedString = HttpUtility.UrlEncode(path);

            //act
            RestResponse response = await ExecuteRequest("Get", "users?"+parameter+"="+encodedString);
            //assert
            Assert.AreEqual(expectedStatusCode, ((int)response.StatusCode).ToString(), $"Status Code is not the Expected, Actual:{(int)response.StatusCode}");
            // Deserialize the response content to a dynamic object
            JToken jsonData = JToken.Parse(response.Content);
            // Verify that the response contains the expected name
            Assert.AreEqual(value, jsonData[0].Value<string>(parameter), "Response does not contain the expected name");
           
        }
    }
}