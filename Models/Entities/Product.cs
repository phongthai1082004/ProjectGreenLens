namespace ProjectGreenLens.Models.Entities
{
    public class Product : BaseEntity
    {
        public string ProductId { get; set; }   // productId từ Google Play
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }      // giá tham chiếu nội bộ
        public string Currency { get; set; }    // VND, USD...
        public ProductType Type { get; set; }   // InApp, Subscription
    }
    public enum ProductType
    {
        InApp = 0,
        Subscription = 1
    }
}
