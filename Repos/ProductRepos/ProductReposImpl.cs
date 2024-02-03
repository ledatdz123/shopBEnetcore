using shopEcomerceExBE.Common.BaseService;
using shopEcomerceExBE.Model;
using shopEcomerceExBE.Model.Product;

namespace shopEcomerceExBE.Repos.ProductRepos
{
    public class ProductReposImpl : IProductRepos
    {
        private readonly IBaseService _baseService;
        public ProductReposImpl(IBaseService baseService)
        {
            this._baseService = baseService;
        }

        public List<AttributeDetail> GetAttributeDetails(int id)
        {
            var obj = _baseService.GetConnection();
            try
            {
                obj.Connect();
                obj.CreateNewStoredProcedure("get_attribute_by_productid");
                obj.AddParameter("@productid", id);
                return obj.ExecStoreProcedureToList<AttributeDetail>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj.Dispose();
                obj.Disconnect();
            }
        }

        public List<ProductImages> GetImages(int id)
        {
            var obj = _baseService.GetConnection();
            try
            {
                obj.Connect();
                obj.CreateNewStoredProcedure("get_image_by_productid");
                obj.AddParameter("@productid", id);
                return obj.ExecStoreProcedureToList<ProductImages>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj.Dispose();
                obj.Disconnect();
            }
        }

        public Product GetProductById(int id)
        {
            var obj = _baseService.GetConnection();
            try
            {
                obj.Connect();
                obj.CreateNewStoredProcedure("get_product_by_id");
                obj.AddParameter("@productid", id);
                return obj.ExecStoreRefToObject<Product>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj.Dispose();
                obj.Disconnect();
            }
        }

        public List<Product> GetProducts()
        {
            var obj = _baseService.GetConnection();
            try
            {
                obj.Connect();
                obj.CreateNewStoredProcedure("get_all_product");
                return obj.ExecStoreProcedureToList<Product>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj.Dispose();
                obj.Disconnect();
            }
        }

        public List<Product> SearchProducts(string productname, string description)
        {
            var obj = _baseService.GetConnection();
            try
            {
                obj.Connect();
                obj.CreateNewStoredProcedure("search_product");
                obj.AddParameter("@productname", productname);
                obj.AddParameter("@description", description);
                return obj.ExecStoreProcedureToList<Product>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj.Dispose();
                obj.Disconnect();
            }
        }
    }
}
