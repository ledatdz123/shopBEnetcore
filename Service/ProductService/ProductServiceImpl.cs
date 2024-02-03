using shopEcomerceExBE.Common.ResponseMessage;
using shopEcomerceExBE.Model;
using shopEcomerceExBE.Model.Product;
using shopEcomerceExBE.Repos.ProductRepos;
using System.Linq;

namespace shopEcomerceExBE.Service.ProductService
{
    public class ProductServiceImpl : IProductService
    {
        private readonly IProductRepos productRepos;
        public ProductServiceImpl(IProductRepos productRepos)
        {
            this.productRepos = productRepos;
        }

        public ResponseMessage GetAllProduct()
        {
            ResponseMessage rp = new ResponseMessage();
            try
            {
                var res = productRepos.GetProducts();
                /*var product = res.GroupBy(x => new { x.productId, x.productName }).Select(i => new ProductResponse
                {
                    productId = i.Key.productId,
                    productName = i.Key.productName,
                    attributes = i.Select(a => new Model.Product.Attribute
                    {
                        attributeid = a.attributeid,
                        attributename = a.attributename,
                        value=a.value,
                    }).ToList().GroupBy(a=>new { a.attributeid, a.attributename }).Select(i=>new Model.Product.Attribute
                    {
                        attributeid=i.Key.attributeid,
                        attributename=i.Key.attributename,
                        attributeProduct=i.ToList(),
                    }).ToList(),
                });*/
                rp.status = MessageStatus.success;
                rp.data = null;
                rp.message = "Lấy danh sách sản phẩm thành công.";
                rp.errorcode = 0;
            }
            catch (Exception ex)
            {
                rp.status = MessageStatus.error;
                rp.message = ex.Message;
                rp.data = null;
                rp.errorcode = -1;
            }
            return rp;
        }

        public ResponseMessage GetProduct()
        {
            ResponseMessage rp = new ResponseMessage();
            try
            {
                var res = productRepos.GetProducts();
                var product=res.GroupBy(x=>new {x.productId, x.productName, x.description, x.price}).Select(p=>new ProductAll
                {
                    productId=p.Key.productId,
                    productName=p.Key.productName,
                    description=p.Key.description,
                    price=p.Key.price,
                    images=p.Select(i=>new ProductImage
                    {
                        imageid=i.imageid,
                        url=i.url,
                    }).ToList(),
                });
                rp.status = MessageStatus.success;
                rp.data = product;
                rp.message = "Lấy danh sách sản phẩm thành công.";
                rp.errorcode = 0;
            }
            catch (Exception ex)
            {
                rp.status = MessageStatus.error;
                rp.message = ex.Message;
                rp.data = null;
                rp.errorcode = -1;
            }
            return rp;
        }

        public ResponseMessage GetProductDetail(int productid)
        {
            ResponseMessage rp = new ResponseMessage();
            try
            {
                var res = productRepos.GetProductById(productid);
                var img = productRepos.GetImages(productid);

                rp.status = MessageStatus.success;
                rp.data = res;
                rp.message = "Lấy danh sách sản phẩm thành công.";
                rp.errorcode = 0;
            }
            catch (Exception ex)
            {
                rp.status = MessageStatus.error;
                rp.message = ex.Message;
                rp.data = null;
                rp.errorcode = -1;
            }
            return rp;
        }

        public ResponseMessage SearchProduct(string productname, string description)
        {

            ResponseMessage rp = new ResponseMessage();
            try
            {
                var res = productRepos.SearchProducts(productname, description);

                rp.status = MessageStatus.success;
                rp.data = res;
                rp.message = "Lấy danh sách sản phẩm thành công.";
                rp.errorcode = 0;
            }
            catch (Exception ex)
            {
                rp.status = MessageStatus.error;
                rp.message = ex.Message;
                rp.data = null;
                rp.errorcode = -1;
            }
            return rp;
        }

        public ResponseMessage Test(int productid)
        {
            ResponseMessage rp = new ResponseMessage();
            try
            {
                ProductDetail productDetail = new ProductDetail();
                var res = productRepos.GetAttributeDetails(productid);
                var attribute=res.GroupBy(x=>x.attributeid).Select(i=>new Attributes
                {
                    attributeid = i.Key,
                    attributeDetail=i.ToList(),
                }).ToList();
                productDetail.attributes = attribute;
                rp.status = MessageStatus.success;
                rp.data = productDetail;
                rp.message = "Lấy danh sách sản phẩm thành công.";
                rp.errorcode = 0;
            }
            catch (Exception ex)
            {
                rp.status = MessageStatus.error;
                rp.message = ex.Message;
                rp.data = null;
                rp.errorcode = -1;
            }
            return rp;
        }
    }
}
