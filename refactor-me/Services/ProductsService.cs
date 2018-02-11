using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_me.Models;
using refactor_me.Repositories;

namespace refactor_me.Services
{
    public class ProductsService
    {
        internal IEnumerable<Product> GetProducts()
        {
            var products = ProductsRepository.GetAllProducts();
            return products;
        }

        internal IEnumerable<Product> GetProductsByName(string name)
        {
            var products = ProductsRepository.GetProductByName(name);
            return products;
        }

        internal Product GetProductById(Guid id)
        {
            var product = ProductsRepository.GetProductById(id);
            return product;
        }

        internal void Create(Product product)
        {
            ProductsRepository.SaveProduct(product);
        }

        internal void Overwrite(Guid id, Product newProduct)
        {
            if (!ProductsRepository.ProductExists(id))
            {
                return;
            }

            ProductsRepository.DeleteProduct(id);
            newProduct.Id = id;
            ProductsRepository.SaveProduct(newProduct);
        }

        internal void Delete(Guid id)
        {
            if (!ProductsRepository.ProductExists(id))
            {
                return;
            }

            ProductsRepository.DeleteProduct(id);
        }
    }
}