using System.Collections.Generic;

namespace Ope_OrderClient
{
    public struct OrderDto
    {
        public List<int> itemIds;
        public int customerId;
        public bool paid;
        public bool shipped;

        public OrderDto(Order order)
        {
            customerId = order.customer.id;
            paid = order.paid;
            shipped = order.shipped;

            itemIds = new List<int>();
            foreach(Item i in order.items)
            {
                itemIds.Add(i.id);
            }
        }
    }
}
