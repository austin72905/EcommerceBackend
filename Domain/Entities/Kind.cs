﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Kind
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // 導航屬性
        public ICollection<ProductKind> ProductKinds { get; set; }
    }
}
