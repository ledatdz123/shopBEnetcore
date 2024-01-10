namespace shopEcomerceExBE.Common.BaseService
{
    public class BaseServiceImpl : IBaseService
    {
        public InitNpgConnection GetConnection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string connectionString = configuration.GetValue<string>("ConnectionStrings:value");
            return InitNpgConnection.CreateData(connectionString);
        }
    }
}
