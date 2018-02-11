using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace refactor_me.Models
{
    public class ProductOption
    {
        public ProductOption()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}