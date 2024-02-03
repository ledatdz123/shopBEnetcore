namespace shopEcomerceExBE.Service.CacheService
{
    public interface ICacheService
    {
        T GetData<T>(string key);
        bool SetData<T> (string key, T value, DateTimeOffset exprirationTime);
        object RemoveData(string key);
    }
}
