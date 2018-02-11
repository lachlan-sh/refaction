using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace refactor_me.Repositories
{
    public static class ProductsRepository
    {
        internal static IEnumerable<Product> GetAllProducts()
        {
            var allProducts = new List<Product>();

            var sql = $"select id from product";
            var rdr = DatabaseHelper.ReadQueryResult(sql);

            while (rdr.Read())
            {
                var id = Guid.Parse(rdr["id"].ToString());
                var product = ProductsRepository.GetProductById(id);
                allProducts.Add(product);
            }

            return allProducts;
        }

        internal static Product GetProductById(Guid id)
        {
            var sql = $"select * from product where id = '{id}'";
            var rdr = DatabaseHelper.ReadQueryResult(sql);

            if (!rdr.Read())
                return null;

            var product = new Product()
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
                Price = decimal.Parse(rdr["Price"].ToString()),
                DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
            };

            return product;
        }

        internal static bool ProductExists(Guid id)
        {
            var product = ProductsRepository.GetProductById(id);
            return product != null;
        }

        public static void SaveProduct(Product newProduct)
        {
            var sql = "insert into product (id, name, description, price, deliveryprice) values " +
                $"('{newProduct.Id}', '{newProduct.Name}', '{newProduct.Description}', {newProduct.Price}, {newProduct.DeliveryPrice})";
            DatabaseHelper.ExecuteSql(sql);
        }

        public static void DeleteProduct(Guid id)
        {
            var productOptions = ProductOptionsRepository.GetAllProductOptions(id);
            foreach (var option in productOptions)
                ProductOptionsRepository.DeleteProductOption(option.Id);

            var sql = $"delete from product where id = '{id}'";
            DatabaseHelper.ExecuteSql(sql);
        }

        internal static IEnumerable<Product> GetProductByName(string name)
        {
            var allMatchingProducts = new List<Product>();

            var sql = $"select id from product where lower(name) like '%{name.ToLower()}%'";
            var rdr = DatabaseHelper.ReadQueryResult(sql);
            
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr["id"].ToString());
                var product = ProductsRepository.GetProductById(id);
                allMatchingProducts.Add(product);
            }

            return allMatchingProducts;
        }
    }
}