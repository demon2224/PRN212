using PE_CE180905_LeAnhTuan.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PE_CE180905_LeAnhTuan.ViewModel
{
    public class BookStoreViewModel : INotifyPropertyChanged
    {
        private readonly BookStoreRespo _repo;

        public ObservableCollection<BookStore> BookStores { get; set; }

        private BookStore? _newBook;
        public BookStore NewBook
        {
            get => _newBook!;
            set
            {
                _newBook = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public BookStoreViewModel()
        {
            _repo = new();
            BookStores = new();
            LoadBookStores();
            NewBook = new BookStore();

            AddCommand = new RelayCommand(AddBook);
            UpdateCommand = new RelayCommand(UpdateBook, CanUpdateBook);
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
            // Do NOT reassign NewBook from CommandParameter; bindings already keep NewBook updated.
            if (NewBook == null ||
                string.IsNullOrWhiteSpace(NewBook.Title) ||
                string.IsNullOrWhiteSpace(NewBook.Author) ||
                string.IsNullOrWhiteSpace(NewBook.Category) ||
                NewBook.Quantity <= 0 ||
                NewBook.Price <= 0)
            {
                MessageBox.Show("Please enter complete and valid information!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _repo.Add(NewBook);
            BookStores.Add(NewBook); // reflect immediately in UI

            MessageBox.Show("Added book successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);

            // Reset input form
            NewBook = new BookStore();
        }

        private void UpdateBook(object? obj)
        {
            if (SelectedBook == null)
            {
                MessageBox.Show("Please select a book to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(SelectedBook.Title) ||
                string.IsNullOrWhiteSpace(SelectedBook.Author) ||
                string.IsNullOrWhiteSpace(SelectedBook.Category) ||
                SelectedBook.Quantity <= 0 ||
                SelectedBook.Price <= 0)
            {
                MessageBox.Show("Please enter complete and valid information before updating!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _repo.Update(SelectedBook);
            MessageBox.Show("Book updated successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
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
                _repo.Delete(SelectedBook);  // delete in database
                BookStores.Remove(SelectedBook);  // remove in UI
                SelectedBook = null;         // reset selection
                MessageBox.Show("Book deleted successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private BookStore? _selectedBook;
        public BookStore? SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
                // Re-evaluate CanExecute for Update/Delete buttons
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private bool CanDeleteBook(object? obj) => SelectedBook != null;
        private bool CanUpdateBook(object? obj) => SelectedBook != null;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}