using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ope_OrderClient
{
    partial class ApiClient
    {
        public async Task<List<Customer>> GetCustomers()
        {
            var req = new RestRequest("/customers");
            return await Execute<List<Customer>>(req);
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var req = new RestRequest("/customers/{id}");
            req.AddUrlSegment("id", id);

            return await Execute<Customer>(req);
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            var req = new RestRequest("/customers", Method.POST);

            req.AddJsonBody(customer);

            return await Execute<Customer>(req);
        }

        public async Task<Customer> PatchCustomer(Customer customer)
        {
            var req = new RestRequest("/customers/{id}", Method.PATCH);

            req.AddUrlSegment("id", customer.id);
            req.AddJsonBody(customer);

            return await Execute<Customer>(req);
        }

        public async Task<Customer> LoginCustomer(Customer customer)
        {
            var req = new RestRequest("/customers/login", Method.POST);

            // hack protoze to jinak nefungovalo...
            req.AddJsonBody(new { customer.firstName, customer.password});

            return await Execute<Customer>(req);
        }

        public void DeleteCustomerById(int id)
        {
            var req = new RestRequest("/customers/{id}", Method.DELETE);
            req.AddUrlSegment("id", id);

            Execute(req);
        }
    }
}
