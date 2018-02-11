using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_me.Models;
using System.Data.SqlClient;

namespace refactor_me.Repositories
{
    public static class ProductOptionsRepository
    {
        internal static IEnumerable<ProductOption> GetAllProductOptions(Guid productId)
        {
            var productOptions = new List<ProductOption>();
           
            var sql = $"select id from productoption where productid = '{productId}'";
            var rdr = DatabaseHelper.ReadQueryResult(sql);

            while (rdr.Read())
            {
                var id = Guid.Parse(rdr["id"].ToString());
                var productOption = ProductOptionsRepository.GetProductOptionById(id);
                productOptions.Add(productOption);
            }

            return productOptions;
        }

        internal static ProductOption GetProductOptionById(Guid productOptionId)
        {
            var sql = $"select * from productoption where id = '{productOptionId}'";
            var rdr = DatabaseHelper.ReadQueryResult(sql);

            if (!rdr.Read())
                return null;

            var productOption = new ProductOption()
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
                ProductId = Guid.Parse(rdr["ProductId"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString()
            };

            return productOption;
        }

        internal static void DeleteProductOption(Guid productOptionId)
        {
            var sql = $"delete from productoption where id = '{productOptionId}'";
            DatabaseHelper.ExecuteSql(sql);
        }

        internal static void SaveProductOption(ProductOption productOption)
        {
            var sql = "insert into productoption (id, productid, name, description) values " +
             $"('{productOption.Id}', '{productOption.ProductId}', '{productOption.Name}', '{productOption.Description}')";
            DatabaseHelper.ExecuteSql(sql);
        }
    }
}