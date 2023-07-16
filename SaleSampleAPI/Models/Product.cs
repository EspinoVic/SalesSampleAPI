namespace SaleSampleAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public float Cost { get; set; }
        public float SalesPrice { get; set; }
        public int InventoryAmount { get; set; }

    }
}
