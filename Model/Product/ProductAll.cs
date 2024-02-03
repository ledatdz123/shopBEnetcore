namespace shopEcomerceExBE.Model.Product
{
    public class ProductAll
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public List<ProductImage> images { get; set; }
    }
    public class ProductImage
    {
        public int imageid { get; set; }
        public string url { get; set; }
    }
}
