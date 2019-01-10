using System.Collections.Generic;

namespace Ope_OrderClient
{
    public class Order
    {
        public int id { get; set; }
        public bool paid { get; set; }
        public bool shipped { get; set; }
        public List<Item> items { get; set; }
        public Customer customer { get; set; }

        public override string ToString()
        {
            string itemNames = "";
            int itemPrice = 0;
            foreach(Item item in items)
            {
                itemNames += $"{item.name}, ";
                itemPrice += item.price;
            }

            return $"{id} - Customer: {customer.firstName} {customer.lastName} Price: {itemPrice} Items: {itemNames} IsPaid: {paid} IsShipped: {shipped}";
        }
    }
}
