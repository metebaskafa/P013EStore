﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P013EStore.Core.Entities
{
    public class Cartline
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
