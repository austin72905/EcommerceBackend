using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class StockCheckDTO
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = "error"; // "success" or "error"
        
        [JsonPropertyName("failed")]
        public List<string> Failed { get; set; } = new();
    }
}
