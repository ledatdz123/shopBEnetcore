namespace shopEcomerceExBE.Model
{
    public class ProductDetail
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public List<ProductImages> images { get; set; }
        public List<Attributes> attributes { get; set; }
    }
    public class ProductImages
    {
        public int imageid { get; set; }
        public string url { get; set; }
    }
    public class Attributes
    {
        public int attributeid { get; set; }
        public List<AttributeDetail> attributeDetail { get; set; }
    }
    public class AttributeDetail
    {
        public int attributeid { get; set; }
        public string attributename { get; set; }
        public string value { get; set; }
    }
}
