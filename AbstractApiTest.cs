using APITesting.Utilities;
using NUnit.Framework;
using RestSharp;

namespace APITest
{
    public abstract class AbstractApiTest
    {

        protected string Url;

        [OneTimeSetUp]
        public void InitVariables()
        {
            Configuration.SetupConfigFile();
            Url = Configuration.ApiUrl;

        }

        protected async Task<RestResponse> ExecuteRequest(string methodType, string apiPath, string data = null)
        {
            RestClient httpClient = new RestClient(Url);
            RestResponse response = new RestResponse();
            RestRequest request;
            

            switch ((Method)Enum.Parse(typeof(Method), methodType))
            {
                case Method.Post:
                    request = new RestRequest(apiPath, Method.Post);
                    request.AddHeader("Authorization", Configuration.ApiToken);
                    request.AddHeader("Accept", "application/json");
                    request.RequestFormat = DataFormat.Json;
                    request.AddParameter("application/json", data, ParameterType.RequestBody);
                    response = await httpClient.ExecutePostAsync(request);
                    break;
                case Method.Get:
                    request = new RestRequest(apiPath);
                    request.AddHeader("Authorization", Configuration.ApiToken);
                    response = await httpClient.ExecuteGetAsync(request);
                    break;
                case Method.Put:
                    request = new RestRequest(apiPath, Method.Put);
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Authorization", Configuration.ApiToken);
                    request.RequestFormat = DataFormat.Json;
                    request.AddParameter("application/json", data, ParameterType.RequestBody);
                    response = await httpClient.ExecutePutAsync(request);
                    break;
                case Method.Delete:
                    request = new RestRequest(apiPath);
                    request.AddHeader("Authorization", Configuration.ApiToken);
                    response = await httpClient.DeleteAsync(request);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return response;
        }


    }

}
