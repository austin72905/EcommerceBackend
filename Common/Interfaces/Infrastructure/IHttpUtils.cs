using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Infrastructure
{
    public interface IHttpUtils
    {
        Task<T> PostFormAsync<T>(string url, Dictionary<string, string> formData,Dictionary<string, string>? headers=null) where T : class;
        Task<T> PostJsonAsync<T>(string url, object jsonData, Dictionary<string, string>? headers = null) where T : class;
    }
}
