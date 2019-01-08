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
    }
}
