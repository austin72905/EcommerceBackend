using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public string MaterialName { get; set; }

        public ICollection<ProductMaterial> ProductMaterials { get; set; }
    }
}
