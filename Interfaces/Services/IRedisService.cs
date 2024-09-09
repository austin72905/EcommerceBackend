namespace EcommerceBackend.Interfaces.Services
{
    public interface IRedisService
    {
        public Task SetUserInfoAsync(string sessonId, string userIndo);

        public Task DelUserInfoAsync(string sessonId);

        public Task<string?> GetUserInfoAsync(string sessonId);

        
    }
}
