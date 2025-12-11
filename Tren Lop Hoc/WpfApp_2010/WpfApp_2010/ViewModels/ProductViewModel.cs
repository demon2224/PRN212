using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApp_2010.Commands;
using WpfApp_2010.Models;
using WpfApp_2010.Services;

namespace WpfApp_2010.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private Product _selectedProduct;
        private string _productName;
        private string _category;
        private string _price;
        private string _stock;
        private string _status;

        public ProductViewModel()
        {
            _productService = new ProductService();
            Products = _productService.GetAllProducts();
            
            // Initialize commands
            AddCommand = new RelayCommand(AddProduct, CanAddProduct);
            EditCommand = new RelayCommand(EditProduct, CanEditProduct);
            DeleteCommand = new RelayCommand(DeleteProduct, CanDeleteProduct);
            
            // Initialize categories and statuses
            Categories = new List<string> { "Electronics", "Clothing", "Books", "Home & Garden", "Sports" };
            Statuses = new List<string> { "Active", "Inactive" };
        }

        #region Properties

        public ObservableCollection<Product> Products { get; }
        
        public List<string> Categories { get; }
        
        public List<string> Statuses { get; }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                    LoadSelectedProductToForm();
                }
            }
        }

        public string ProductName
        {
            get => _productName;
            set
            {
                if (_productName != value)
                {
                    _productName = value;
                    OnPropertyChanged(nameof(ProductName));
                }
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        public string Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public string Stock
        {
            get => _stock;
            set
            {
                if (_stock != value)
                {
                    _stock = value;
                    OnPropertyChanged(nameof(Stock));
                }
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public string ProductId => SelectedProduct?.ProductId.ToString() ?? _productService.GetNextProductId().ToString();

        #endregion

        #region Commands

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        #endregion

        #region Command Methods

        private bool CanAddProduct(object parameter)
        {
            return !string.IsNullOrWhiteSpace(ProductName) &&
                   !string.IsNullOrWhiteSpace(Category) &&
                   !string.IsNullOrWhiteSpace(Price) &&
                   !string.IsNullOrWhiteSpace(Stock) &&
                   !string.IsNullOrWhiteSpace(Status);
        }

        private void AddProduct(object parameter)
        {
            try
            {
                if (!ValidateInput()) return;

                var newProduct = new Product
                {
                    ProductName = ProductName.Trim(),
                    Category = Category,
                    Price = decimal.Parse(Price),
                    Stock = int.Parse(Stock),
                    Status = Status
                };

                _productService.AddProduct(newProduct);
                ClearForm();
                ShowMessage("Product added successfully!", "Success", MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                ShowMessage($"Error adding product: {ex.Message}", "Error", MessageBoxImage.Error);
            }
        }

        private bool CanEditProduct(object parameter)
        {
            return SelectedProduct != null &&
                   !string.IsNullOrWhiteSpace(ProductName) &&
                   !string.IsNullOrWhiteSpace(Category) &&
                   !string.IsNullOrWhiteSpace(Price) &&
                   !string.IsNullOrWhiteSpace(Stock) &&
                   !string.IsNullOrWhiteSpace(Status);
        }

        private void EditProduct(object parameter)
        {
            try
            {
                if (SelectedProduct == null)
                {
                    ShowMessage("Please select a product to edit.", "No Selection", MessageBoxImage.Warning);
                    return;
                }

                if (!ValidateInput()) return;

                var updatedProduct = new Product
                {
                    ProductId = SelectedProduct.ProductId,
                    ProductName = ProductName.Trim(),
                    Category = Category,
                    Price = decimal.Parse(Price),
                    Stock = int.Parse(Stock),
                    Status = Status
                };

                _productService.UpdateProduct(updatedProduct);
                ShowMessage("Product updated successfully!", "Success", MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                ShowMessage($"Error updating product: {ex.Message}", "Error", MessageBoxImage.Error);
            }
        }

        private bool CanDeleteProduct(object parameter)
        {
            return SelectedProduct != null;
        }

        private void DeleteProduct(object parameter)
        {
            try
            {
                if (SelectedProduct == null)
                {
                    ShowMessage("Please select a product to delete.", "No Selection", MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show($"Are you sure you want to delete '{SelectedProduct.ProductName}'?",
                                           "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _productService.DeleteProduct(SelectedProduct);
                    ClearForm();
                    ShowMessage("Product deleted successfully!", "Success", MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Error deleting product: {ex.Message}", "Error", MessageBoxImage.Error);
            }
        }

        #endregion

        #region Helper Methods

        private void LoadSelectedProductToForm()
        {
            if (SelectedProduct != null)
            {
                ProductName = SelectedProduct.ProductName;
                Category = SelectedProduct.Category;
                Price = SelectedProduct.Price.ToString();
                Stock = SelectedProduct.Stock.ToString();
                Status = SelectedProduct.Status;
                OnPropertyChanged(nameof(ProductId));
            }
        }

        private void ClearForm()
        {
            ProductName = string.Empty;
            Category = string.Empty;
            Price = string.Empty;
            Stock = string.Empty;
            Status = string.Empty;
            SelectedProduct = null;
            OnPropertyChanged(nameof(ProductId));
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(ProductName) ||
                string.IsNullOrWhiteSpace(Category) ||
                string.IsNullOrWhiteSpace(Price) ||
                string.IsNullOrWhiteSpace(Stock) ||
                string.IsNullOrWhiteSpace(Status))
            {
                ShowMessage("Please fill in all fields.", "Validation Error", MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(Price, out _))
            {
                ShowMessage("Please enter a valid price.", "Validation Error", MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(Stock, out _))
            {
                ShowMessage("Please enter a valid stock number.", "Validation Error", MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void ShowMessage(string message, string title, MessageBoxImage icon)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, icon);
        }

        #endregion
    }
}