using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json;
using refactor_me.Repositories;

namespace refactor_me.Models
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
    }
}