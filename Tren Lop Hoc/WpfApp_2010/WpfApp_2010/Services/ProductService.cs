using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp_2010.Models;

namespace WpfApp_2010.Services
{
    public interface IProductService
    {
        ObservableCollection<Product> GetAllProducts();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        int GetNextProductId();
    }

    public class ProductService : IProductService
    {
        private readonly ObservableCollection<Product> _products;
        private int _nextProductId = 1;

        public ProductService()
        {
            _products = new ObservableCollection<Product>
            {
                new Product { ProductId = _nextProductId++, ProductName = "Laptop Dell XPS 13", Category = "Electronics", Price = 999.99m, Stock = 15, Status = "Active" },
                new Product { ProductId = _nextProductId++, ProductName = "Wireless Mouse", Category = "Electronics", Price = 29.99m, Stock = 50, Status = "Active" },
                new Product { ProductId = _nextProductId++, ProductName = "Programming Book", Category = "Books", Price = 45.00m, Stock = 20, Status = "Active" },
                new Product { ProductId = _nextProductId++, ProductName = "Office Chair", Category = "Home & Garden", Price = 199.99m, Stock = 8, Status = "Inactive" }
            };
        }

        public ObservableCollection<Product> GetAllProducts()
        {
            return _products;
        }

        public void AddProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            
            product.ProductId = _nextProductId++;
            _products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            
            var existingProduct = _products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Category = product.Category;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                existingProduct.Status = product.Status;
            }
        }

        public void DeleteProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            
            _products.Remove(product);
        }

        public int GetNextProductId()
        {
            return _nextProductId;
        }
    }
}