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
using WPF_Study.Models;
using WPF_Study.View;

namespace WPF_Study.ViewModel
{
    public class SachViewModel : INotifyPropertyChanged

    {
        private readonly BookRespo _repo;
        private readonly CategoriesRespo _categories;

        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<BookCategory> Categories { get; set; }

        private Book? _selectedBook;
        public Book? SelectedBook
        {
            get => _selectedBook;
            set { _selectedBook = value; OnPropertyChanged(); }
        }

        private Book _newBook;
        public Book NewBook
        {
            get => _newBook;
            set
            {
                _newBook = value;
                OnPropertyChanged();
            }
        }

        // Khai Báo Ba Hàm ADD Book
        public ICommand OpenAddBookCommand { get; }
        public ICommand AddBookCommand { get; }
        public ICommand CloseAddBookWindowCommand { get; }

        // Khai Báo Ba Hàm Update Book
        public ICommand OpenUpdateBookCommand { get; }
        public ICommand UpdateBookCommand { get; }
        public ICommand CloseUpdateBookWindowCommand { get; }

        // Khai Báo Hàm Delete Book
        public ICommand DeleteBookCommand { get; }


        private AddBookWindow? _addBookWindow;
        private UpdateBookWindow? _updateBookWindow;


        public event PropertyChangedEventHandler? PropertyChanged; // Cho phép null

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Contructor
        public SachViewModel()
        {
            _repo = new();
            _categories = new();
            Books = new();
            Categories = new();
            LoadBooks();
            LoadCategories();
            _newBook = new();

            // Chức Năng Add Book
            OpenAddBookCommand = new RelayCommand(OpenAddBookWindow);
            AddBookCommand = new RelayCommand(AddBook);
            CloseAddBookWindowCommand = new RelayCommand(CloseAddBookWindow);

            // Chức Năng Update Book
            OpenUpdateBookCommand = new RelayCommand(OpenUpdateBookWindow, CanUpdateBook);
            UpdateBookCommand = new RelayCommand(UpdateBook, CanUpdateBook);
            CloseUpdateBookWindowCommand = new RelayCommand(CloseUpdateBookWindow);

            // Chức Năng Delete Book
            DeleteBookCommand = new RelayCommand(DeleteBook, CanDeleteBook);
        }

        // Hàm load dữ liệu
        private void LoadBooks()
        {
            Books.Clear();
            var bookList = _repo.Books();

            foreach (var book in bookList)
            {
                Console.WriteLine($"Book: {book.BookName}, Category: {book.BookCategory?.BookGenreType ?? "NULL"}");
                Books.Add(book);
            }
        }

        private void LoadCategories()
        {
            Categories.Clear();
            foreach (var category in _categories.GetBookCategories())
            {
                Categories.Add(category);
            }
        }

        private void OpenAddBookWindow(object? obj)
        {
            NewBook = new Book(); // Reset NewBook trước khi mở form
            _addBookWindow = new AddBookWindow { DataContext = this };
            _addBookWindow.ShowDialog();
        }

        private void OpenUpdateBookWindow(object? obj)
        {
            if (SelectedBook == null)
            {
                MessageBox.Show("Please select a book to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewBook = new Book();
            _updateBookWindow = new UpdateBookWindow { DataContext = this };
            _updateBookWindow.ShowDialog();
        }

        // Hàm ADD Book
        private void AddBook(object? obj)
        {
            if (string.IsNullOrWhiteSpace(NewBook.BookName) ||
                string.IsNullOrWhiteSpace(NewBook.Description) ||
                NewBook.PublicationDate == default ||
                NewBook.Quantity <= 0 ||
                NewBook.Price <= 0 ||
                string.IsNullOrWhiteSpace(NewBook.Author) ||
                NewBook.BookCategoryId <= 0)
            {
                MessageBox.Show("Please enter complete information!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _repo.Add(NewBook);
            Books.Add(NewBook); // Cập nhật UI ngay sau khi thêm sách
            MessageBox.Show("Added book successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            CloseAddBookWindow(null);
        }

        // Hàm Update Book
        private void UpdateBook(object? obj)
        {
            if (SelectedBook == null) return;

            _repo.Update(SelectedBook); // Cập nhật trong database
            LoadBooks(); // Load lại danh sách
            MessageBox.Show("Book updated successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            CloseUpdateBookWindow(null);
        }

        // Hàm Delete Book
        private void DeleteBook(object? obj)
        {
            if (SelectedBook == null)
            {
                MessageBox.Show("Please select a book to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedBook.BookName}'?",
                                         "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _repo.Delete(SelectedBook);  // Xóa trong database
                Books.Remove(SelectedBook);  // Xóa trên UI
                SelectedBook = null;         // Reset SelectedBook
                MessageBox.Show("Book deleted successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool CanUpdateBook(object? obj) => SelectedBook != null;

        private bool CanDeleteBook(object? obj) => SelectedBook != null;


        private void CloseUpdateBookWindow(object? obj)
        {
            _updateBookWindow?.Close();
        }

        private void CloseAddBookWindow(object? obj)
        {
            _addBookWindow?.Close();
        }
    }
}
