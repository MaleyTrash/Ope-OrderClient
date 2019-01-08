using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ope_OrderClient
{
    partial class ApiClient
    {
        public async Task<List<Order>> GetOrders()
        {
            var req = new RestRequest("/orders");
            return await Execute<List<Order>>(req);
        }

        public async Task<Order> GetOrderById(int id)
        {
            var req = new RestRequest("/orders/{id}");
            req.AddUrlSegment("id", id);

            return await Execute<Order>(req);
        }

        public async Task<Order> CreateOrder(Order order)
        {
            var req = new RestRequest("/orders", Method.POST);

            var body = new OrderDto(order);

            req.AddJsonBody(body);

            return await Execute<Order>(req);
        }

        public async Task<Order> PatchOrder(Order order)
        {
            var req = new RestRequest("/orders/{id}", Method.PATCH);

            var body = new OrderDto(order);

            req.AddUrlSegment("id", order.id);
            req.AddJsonBody(body);

            return await Execute<Order>(req);
        }

        public void DeleteOrderById(int id)
        {
            var req = new RestRequest("/orders/{id}", Method.DELETE);
            req.AddUrlSegment("id", id);

            Execute(req);
        }
    }
}
