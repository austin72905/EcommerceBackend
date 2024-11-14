

using Application.DTOs;

namespace Application.DummyData
{
    public static class ProductList
    {
        public static List<ProductWithCountDTO> ProductListInOrder = new List<ProductWithCountDTO>()
        {
            new ProductWithCountDTO()
            {
                Count = 2,
                Product=new ProductInfomationDTO
                {
                    Title="超時尚流蘇几皮外套",
                    ProductId=26790367,
                    //Stock=60,
                    //Price=100,
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
                SelectedVariant=new ProductVariantDTO
                        {
                            VariantID=3,
                            Color="米",
                            Size="L",
                            SKU="WHEAT-L",
                            Stock=3,
                            Price=150
                        },

            },
            new ProductWithCountDTO()
            {
                Count = 3,
                Product= new ProductInfomationDTO
                {
                    Title="紫色格紋大衣",
                    ProductId=26790368,
                    //Stock=5,
                    //Price=598,
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
                SelectedVariant=new ProductVariantDTO
                {
                    VariantID=5,
                    Color="咖啡",
                    Size="L",
                    SKU="BROWN-L",
                    Stock=20,
                    Price=211
                }

            },
            new ProductWithCountDTO()
            {
                Count = 3,
                Product=new ProductInfomationDTO
                {
                    Title="超質感綠色皮衣",
                    ProductId=13790367,
                    //Stock=18,
                    //Price=179,
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
                SelectedVariant= new ProductVariantDTO
                {
                    VariantID=4,
                    Color="咖啡",
                    Size="M",
                    SKU="BROWN-M",
                    Stock=17,
                    Price=199
                },

            },
        };
    }
}
