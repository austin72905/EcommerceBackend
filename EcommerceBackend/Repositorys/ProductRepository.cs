using EcommerceBackend.Interfaces.Repositorys;
using EcommerceBackend.Models;
using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Repositorys
{
    public class ProductRepository : IProductRepository
    {
        public ProductInfomationDTO? GetProductById(int productId)
        {
            return fakeProductList.FirstOrDefault(p => p.ProductId == productId);
        }

        public List<ProductInfomationDTO> GetProductsByKind(string kind)
        {
             return fakeProductList;
        }

        public List<ProductInfomationDTO> GetProductsByTag(string tag)
        {
           
            return fakeProductList;
        }


        public static List<ProductInfomationDTO> fakeProductList= new List<ProductInfomationDTO>()
            {
                new ProductInfomationDTO
                {
                    Title="超時尚流蘇几皮外套",
                    ProductId=26790367,
                    Stock=60,
                    Price=150,
                    DiscountPrice=100,
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    Variants=new List<ProductVariantDTO>
                    {
                       new ProductVariantDTO
                        {
                            VariantID=1,
                            Color="黑",
                            Size="S",
                            SKU="BLACK-S",
                            Stock=2,
                            Price=99
                        },
                        new ProductVariantDTO
                        {
                            VariantID=2,
                            Color="黑",
                            Size="L",
                            SKU="BLACK-L",
                            Stock=16,
                            Price=283
                        },
                        new ProductVariantDTO
                        {
                            VariantID=3,
                            Color="米",
                            Size="L",
                            SKU="WHEAT-L",
                            Stock=3,
                            Price=150
                        },
                        new ProductVariantDTO
                        {
                            VariantID=4,
                            Color="咖啡",
                            Size="M",
                            SKU="BROWN-M",
                            Stock=17,
                            Price=199
                        },
                        new ProductVariantDTO
                        {
                            VariantID=5,
                            Color="咖啡",
                            Size="L",
                            SKU="BROWN-L",
                            Stock=20,
                            Price=211
                        }
                    },
                    CoverImg="http://localhost:9000/coat1.jpg"
                },
                new ProductInfomationDTO
                {
                    Title="紫色格紋大衣",
                    ProductId=26790368,
                    Stock=5,
                    Price=598,
                    DiscountPrice=500,
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    Variants=new List<ProductVariantDTO>
                    {
                        new ProductVariantDTO
                        {
                            VariantID=1,
                            Color="黑",
                            Size="S",
                            SKU="BLACK-S",
                            Stock=2,
                            Price=99
                        },
                        new ProductVariantDTO
                        {
                            VariantID=2,
                            Color="黑",
                            Size="L",
                            SKU="BLACK-L",
                            Stock=16,
                            Price=283
                        },
                        new ProductVariantDTO
                        {
                            VariantID=3,
                            Color="米",
                            Size="L",
                            SKU="WHEAT-L",
                            Stock=3,
                            Price=150
                        },
                        new ProductVariantDTO
                        {
                            VariantID=4,
                            Color="咖啡",
                            Size="M",
                            SKU="BROWN-M",
                            Stock=17,
                            Price=199
                        },
                        new ProductVariantDTO
                        {
                            VariantID=5,
                            Color="咖啡",
                            Size="L",
                            SKU="BROWN-L",
                            Stock=20,
                            Price=211
                        }
                    },
                    CoverImg="http://localhost:9000/coat4.jpg"
                },
                new ProductInfomationDTO
                {
                    Title="超質感綠色皮衣",
                    ProductId=13790367,
                    Stock=18,
                    Price=179,
                    DiscountPrice=159,
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    Variants=new List<ProductVariantDTO>
                    {
                        new ProductVariantDTO
                        {
                            VariantID=1,
                            Color="黑",
                            Size="S",
                            SKU="BLACK-S",
                            Stock=2,
                            Price=99
                        },
                        new ProductVariantDTO
                        {
                            VariantID=2,
                            Color="黑",
                            Size="L",
                            SKU="BLACK-L",
                            Stock=16,
                            Price=283
                        },
                        new ProductVariantDTO
                        {
                            VariantID=3,
                            Color="米",
                            Size="L",
                            SKU="WHEAT-L",
                            Stock=3,
                            Price=150
                        },
                        new ProductVariantDTO
                        {
                            VariantID=4,
                            Color="咖啡",
                            Size="M",
                            SKU="BROWN-M",
                            Stock=17,
                            Price=199
                        },
                        new ProductVariantDTO
                        {
                            VariantID=5,
                            Color="咖啡",
                            Size="L",
                            SKU="BROWN-L",
                            Stock=20,
                            Price=211
                        }
                    },
                    CoverImg="http://localhost:9000/coat3.jpg"
                },
                new ProductInfomationDTO
                {
                    Title="海島風情黑色短袖襯衫",
                    ProductId=33790012,
                    Stock=60,
                    Price=100,
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    Variants=new List<ProductVariantDTO>
                    {
                        new ProductVariantDTO
                        {
                            VariantID=1,
                            Color="黑",
                            Size="S",
                            SKU="BLACK-S",
                            Stock=2,
                            Price=99
                        },
                        new ProductVariantDTO
                        {
                            VariantID=2,
                            Color="黑",
                            Size="L",
                            SKU="BLACK-L",
                            Stock=16,
                            Price=283
                        },
                        new ProductVariantDTO
                        {
                            VariantID=3,
                            Color="米",
                            Size="L",
                            SKU="WHEAT-L",
                            Stock=3,
                            Price=150
                        },
                        new ProductVariantDTO
                        {
                            VariantID=4,
                            Color="咖啡",
                            Size="M",
                            SKU="BROWN-M",
                            Stock=17,
                            Price=199
                        },
                        new ProductVariantDTO
                        {
                            VariantID=5,
                            Color="咖啡",
                            Size="L",
                            SKU="BROWN-L",
                            Stock=20,
                            Price=211
                        }
                    },
                    CoverImg="http://localhost:9000/coat2.jpg"
                },
                new ProductInfomationDTO
                {
                    Title="帥氣單寧",
                    ProductId=34690012,
                    Stock=60,
                    Price=799,
                    DiscountPrice=599,
                    Material=new List<string>{ "聚酯纖維", "聚氨酯纖維"},
                    HowToWash="洗衣機（水溫40度）, 不可乾洗, 不可烘乾。本商品會在流汗或淋雨弄濕時，或因摩擦而染色到其他衣物上，敬請注意。",
                    Features="其實我也不知道要說什麼...a 其實我也不知道要說什麼...a 其實我也不知道要說什麼...a",
                    Images=new List<string>(),
                    Variants=new List<ProductVariantDTO>
                    {
                       new ProductVariantDTO
                        {
                            VariantID=1,
                            Color="黑",
                            Size="S",
                            SKU="BLACK-S",
                            Stock=2,
                            Price=99,
                            DiscountPrice=22
                        },
                        new ProductVariantDTO
                        {
                            VariantID=2,
                            Color="黑",
                            Size="L",
                            SKU="BLACK-L",
                            Stock=16,
                            Price=283
                        },
                        new ProductVariantDTO
                        {
                            VariantID=3,
                            Color="米",
                            Size="L",
                            SKU="WHEAT-L",
                            Stock=3,
                            Price=150
                        },
                        new ProductVariantDTO
                        {
                            VariantID=4,
                            Color="咖啡",
                            Size="M",
                            SKU="BROWN-M",
                            Stock=17,
                            Price=199
                        },
                        new ProductVariantDTO
                        {
                            VariantID=5,
                            Color="咖啡",
                            Size="L",
                            SKU="BROWN-L",
                            Stock=20,
                            Price=211
                        }
                    },
                    CoverImg="http://localhost:9000/coat5.jpg"
                },

            };
    }
}
