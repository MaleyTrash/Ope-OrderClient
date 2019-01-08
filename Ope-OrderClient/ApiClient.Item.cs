using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ope_OrderClient
{
    partial class ApiClient
    {
        public async Task<List<Item>> GetItems()
        {
            var req = new RestRequest("/items");
            return await Execute<List<Item>>(req);
        }

        public async Task<Item> GetItemById(int id)
        {
            var req = new RestRequest("/items/{id}");
            req.AddUrlSegment("id", id);

            return await Execute<Item>(req);
        }

        public async Task<Item> CreateItem(Item item)
        {
            var req = new RestRequest("/items", Method.POST);

            req.AddJsonBody(item);

            return await Execute<Item>(req);
        }

        public async Task<Item> PatchItem(Item item)
        {
            var req = new RestRequest("/items/{id}", Method.PATCH);

            req.AddUrlSegment("id", item.id);
            req.AddJsonBody(item);

            return await Execute<Item>(req);
        }

        public void DeleteItemById(int id)
        {
            var req = new RestRequest("/items/{id}", Method.DELETE);
            req.AddUrlSegment("id", id);

            Execute(req);
        }
    }
}
