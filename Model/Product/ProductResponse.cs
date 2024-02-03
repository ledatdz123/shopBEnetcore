namespace shopEcomerceExBE.Model.Product
{
    public class ProductResponse
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public int attributeid { get; set; }
        public List<Attribute> attributes { get; set; }
    }
    /*public class Attribute
    {
        public int attributeid { get; set; }
        public string attributename { get; set; }
        public string value { get; set; }
    }*/
    public class Attribute
    {
        public int attributeid { get; set; }
        public string attributename { get; set; }
        public string value { get; set; }
        public List <Attribute> attributeProduct { get; set; }
    }
}
