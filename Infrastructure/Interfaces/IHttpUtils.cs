using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IHttpUtils
    {
        Task<T> PostFormAsync<T>(string url, Dictionary<string, string> formData,Dictionary<string, string>? headers=null) where T : class;
    }
}
