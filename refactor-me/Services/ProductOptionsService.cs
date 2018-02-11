using refactor_me.Models;
using refactor_me.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Services
{
    public class ProductOptionsService
    {
        internal IEnumerable<ProductOption> GetProductOptions(Guid productId)
        {
            return ProductOptionsRepository.GetAllProductOptions(productId);
        }

        internal ProductOption GetProductOptionById(Guid productOptionId)
        {
            var option = ProductOptionsRepository.GetProductOptionById(productOptionId);
            return option;
        }

        internal void Create(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            ProductOptionsRepository.SaveProductOption(option);
        }

        internal void Overwrite(Guid productOptionId, ProductOption newProductOption)
        {
            var originalProductOption = ProductOptionsRepository.GetProductOptionById(productOptionId);
            if (originalProductOption == null)
            {
                return;
            }

            ProductOptionsRepository.DeleteProductOption(productOptionId);
            newProductOption.Id = productOptionId;
            ProductOptionsRepository.SaveProductOption(newProductOption);
        }

        internal void Delete(Guid productId, Guid productOptionId)
        {
            ProductOptionsRepository.DeleteProductOption(productOptionId);
        }

        internal bool ProductHasOption(Guid productId, Guid optionId)
        {
            var productOption = ProductOptionsRepository.GetProductOptionById(optionId);
            if (productOption == null)
            {
                return false;
            }
            if (productOption.ProductId != productId)
            {
                return false;

            }
            return true;
        }
    }
}