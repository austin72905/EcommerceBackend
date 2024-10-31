using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IRedisService
    {
        public Task SetUserInfoAsync(string sessonId, string userInfo);

        public Task DelUserInfoAsync(string sessonId);

        public Task<string?> GetUserInfoAsync(string sessonId);


    }
}
