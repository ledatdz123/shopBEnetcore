using shopEcomerceExBE.Common.ResponseMessage;

namespace shopEcomerceExBE.Service.ProductService
{
    public interface IProductService
    {
        public ResponseMessage GetProduct();
        public ResponseMessage GetAllProduct();
        public ResponseMessage GetProductDetail(int productid);
        public ResponseMessage Test(int productid);
        public ResponseMessage SearchProduct(string productname, string description);
    }
}
