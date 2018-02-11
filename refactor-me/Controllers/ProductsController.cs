using System;
using System.Net;
using System.Web.Http;
using refactor_me.Models;
using refactor_me.Services;
using System.Collections.Generic;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private ProductsService productsService;
        private ProductOptionsService productOptionsService;

        public ProductsController()
        {
            productsService = new ProductsService();
            productOptionsService = new ProductOptionsService();
        }

        [Route]
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return productsService.GetProducts();
        }

        [Route("name/{name}")]
        [HttpGet]
        public IEnumerable<Product> SearchByName(string name)
        {
            return productsService.GetProductsByName(name);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            var product = productsService.GetProductById(id);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return product;
        }

        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            productsService.Create(product);
        }

        [Route("{id}")]
        [HttpPut]
        public void Overwrite(Guid id, Product product)
        {
            productsService.Overwrite(id, product);
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            productsService.Delete(id);
        }

        [Route("{productId}/options")]
        [HttpGet]
        public IEnumerable<ProductOption> GetOptions(Guid productId)
        {
            return productOptionsService.GetProductOptions(productId);
        }

        [Route("{productId}/options/{optionId}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid optionId)
        {
            ThrowIfIdPairInvalid(productId, optionId);

            var option = productOptionsService.GetProductOptionById(optionId);
            if (option == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return option;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            productOptionsService.Create(productId, option);
        }

        [Route("{productId}/options/{optionId}")]
        [HttpPut]
        public void OverwriteOption(Guid productId, Guid optionId, ProductOption option)
        {
            ThrowIfIdPairInvalid(productId, optionId);

            productOptionsService.Overwrite(optionId, option);
        }

        [Route("{productId}/options/{optionId}")]
        [HttpDelete]
        public void DeleteOption(Guid productId, Guid optionId)
        {
            ThrowIfIdPairInvalid(productId, optionId);

            productOptionsService.Delete(optionId, optionId);
        }

        private void ThrowIfIdPairInvalid(Guid productId, Guid optionId)
        {
            var optionExistsForGivenProduct = productOptionsService.ProductHasOption(productId, optionId);
            if (!optionExistsForGivenProduct)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}
