using shopEcomerceExBE.Model;
using shopEcomerceExBE.Model.Product;

namespace shopEcomerceExBE.Repos.ProductRepos
{
    public interface IProductRepos
    {
        public List<Product> GetProducts();
        public Product GetProductById(int id);
        public List<ProductImages> GetImages(int id);
        public List<AttributeDetail> GetAttributeDetails(int id);
        public List<Product> SearchProducts(string productname, string description);
    }
}
