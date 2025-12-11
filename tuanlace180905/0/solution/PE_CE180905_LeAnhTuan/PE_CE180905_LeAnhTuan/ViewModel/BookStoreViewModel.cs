using PE_CE180905_LeAnhTuan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace PE_CE180905_LeAnhTuan.ViewModel
{
    public class BookStoreViewModel : INotifyPropertyChanged
    {
        private readonly BookStoreRespo _repo;

        public ObservableCollection<BookStore> BookStores { get; set; }

        private BookStore? _newBook;
        public BookStore NewBook
        {
            get => _newBook;
            set
            {
                _newBook = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }



        public BookStoreViewModel()
        {
            _repo = new();
            BookStores = new();
            LoadBookStores();
            NewBook = new BookStore();
            AddCommand = new RelayCommand(AddBook);

            DeleteCommand = new RelayCommand(DeleteBook, CanDeleteBook);
        }

        private void LoadBookStores()
        {
            BookStores.Clear();
            var bookingList = _repo.GetAllBook();

            foreach (var book in bookingList)
            {
                BookStores.Add(book);
            }
        }
        private void AddBook(object? obj)
        {
            
            if (string.IsNullOrWhiteSpace(NewBook.Title) ||
                string.IsNullOrWhiteSpace(NewBook.Author) ||
                string.IsNullOrWhiteSpace(NewBook.Category) ||
                NewBook.Quantity == null || NewBook.Quantity <= 0 ||
                NewBook.Price == null || NewBook.Price <= 0)

            {
                MessageBox.Show("Please enter complete information!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _repo.Add(NewBook);
            BookStores.Add(NewBook); // Cập nhật UI ngay sau khi thêm sách
            MessageBox.Show("Added book successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteBook(object? obj)
        {
            if (SelectedBook == null)
            {
                MessageBox.Show("Please select a book to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedBook.Title}'?",
                                         "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _repo.Delete(SelectedBook);  // Xóa trong database
                BookStores.Remove(SelectedBook);  // Xóa trên UI
                SelectedBook = null;         // Reset SelectedBook
                MessageBox.Show("Book deleted successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private BookStore? _selectedBook;
        public BookStore? SelectedBook
        {
            get => _selectedBook;
            set { _selectedBook = value; OnPropertyChanged(); }
        }


        private bool CanDeleteBook(object? obj) => SelectedBook != null;

        public event PropertyChangedEventHandler? PropertyChanged; // Cho phép null

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
