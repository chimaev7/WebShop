﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop2024.Infrastructure.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string CategoryName { get; set; } = null!;

        public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
