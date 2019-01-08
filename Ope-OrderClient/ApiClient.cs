using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ope_OrderClient
{
    partial class ApiClient
    {
        public const string API_URL = "https://opel.xn--mp8hal61bd.ws";

        private RestClient client = new RestClient(API_URL);

        private void Execute(IRestRequest req)
        {
            client.Execute(req);
        }

        private async Task<T> Execute<T>(IRestRequest req)
        {
            var res = await client.ExecuteTaskAsync<T>(req);

            return res.Data;
        }
    }
}
