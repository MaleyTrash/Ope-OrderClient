namespace Ope_OrderClient
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }

        public override string ToString()
        {
            return $"{id} - {name} - {price}";
        }
    }
}
