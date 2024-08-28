using EcommerceBackend.Interfaces.Repositorys;
using EcommerceBackend.Models;

namespace EcommerceBackend.Repositorys
{
    public class ProductRepository : IProductRepository
    {    
        public List<ProductInfomation> GetProductsByKind(string kind)
        {
            var products = new List<ProductInfomation>()
            {
                new ProductInfomation
                {
                    Title="超時尚流蘇几皮外套",
                    ProductId="26790367",
                    Stock=60,
                    Price=100,
                    Size=new List<string>{ "S", "M", "L", "XL", "2XL", "3XL" },
                    Color=new List<string>{ "black", "wheat", "brown" },
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    CoverImg=""
                },
                new ProductInfomation
                {
                    Title="紫色格紋大衣",
                    ProductId="26790368",
                    Stock=5,
                    Price=598,
                    Size=new List<string>{ "S", "M", "L", "XL", "2XL"},
                    Color=new List<string>{ "black", "wheat", "purple" },
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    CoverImg=""
                },
                new ProductInfomation
                {
                    Title="超質感綠色皮衣",
                    ProductId="13790367",
                    Stock=18,
                    Price=179,
                    Size=new List<string>{ "S", "M", "L", "XL", "2XL", "3XL"},
                    Color=new List<string>{ "black", "wheat", "brown" },
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    CoverImg=""
                },
                new ProductInfomation
                {
                    Title="海島風情黑色短袖襯衫",
                    ProductId="33790012",
                    Stock=60,
                    Price=100,
                    Size=new List<string>{ "S", "M", "L", "XL"},
                    Color=new List<string>{ "black" },
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    CoverImg=""
                },
                new ProductInfomation
                {
                    Title="帥氣單寧",
                    ProductId="34690012",
                    Stock=60,
                    Price=799,
                    Size=new List<string>{"S", "M", "L", "XL" },
                    Color=new List<string>{ "black","blue" },
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    CoverImg=""
                },

            };
            return products;
        }

        public List<ProductInfomation> GetProductsByTag(string tag)
        {
            var products = new List<ProductInfomation>()
            {
                new ProductInfomation
                {
                    Title="超時尚流蘇几皮外套",
                    ProductId="26790367",
                    Stock=60,
                    Price=100,
                    Size=new List<string>{ "S", "M", "L", "XL", "2XL", "3XL" },
                    Color=new List<string>{ "black", "wheat", "brown" },
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    CoverImg=""
                },
                new ProductInfomation
                {
                    Title="紫色格紋大衣",
                    ProductId="26790368",
                    Stock=5,
                    Price=598,
                    Size=new List<string>{ "S", "M", "L", "XL", "2XL"},
                    Color=new List<string>{ "black", "wheat", "purple" },
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    CoverImg=""
                },
                new ProductInfomation
                {
                    Title="超質感綠色皮衣",
                    ProductId="13790367",
                    Stock=18,
                    Price=179,
                    Size=new List<string>{ "S", "M", "L", "XL", "2XL", "3XL"},
                    Color=new List<string>{ "black", "wheat", "brown" },
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    CoverImg=""
                },
                new ProductInfomation
                {
                    Title="海島風情黑色短袖襯衫",
                    ProductId="33790012",
                    Stock=60,
                    Price=100,
                    Size=new List<string>{ "S", "M", "L", "XL"},
                    Color=new List<string>{ "black" },
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    CoverImg=""
                },
                new ProductInfomation
                {
                    Title="帥氣單寧",
                    ProductId="34690012",
                    Stock=60,
                    Price=799,
                    Size=new List<string>{"S", "M", "L", "XL" },
                    Color=new List<string>{ "black","blue" },
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    CoverImg=""
                },

            };
            return products;
        }
    }
}
