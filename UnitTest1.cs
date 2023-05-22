using APITest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RestSharp;
using System.ComponentModel;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace APITesting
{
    [TestClass]
    public class UnitTest1 : AbstractApiTest
    {
        [Test]
        [TestCase( "200")]
        public async Task APIGetDashboardByNameAsync( string expectedStatusCode)
        {
          
        
            //act
            RestResponse response = await ExecuteRequest("Get", "users");
            //assert
            Assert.AreEqual(expectedStatusCode, ((int)response.StatusCode).ToString(), $"Status Code is not the Expected, Actual:{(int)response.StatusCode}");
                 }
    }
}