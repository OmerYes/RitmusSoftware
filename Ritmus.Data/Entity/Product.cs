﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmus.Data.Entity
{
   public class Product:BaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string ImageURL { get; set; }
        public virtual List<Chart> Charts { get; set; }
    }
}
