using PETest1.Models;
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
using static System.Reflection.Metadata.BlobBuilder;

namespace PETest1.ViewModel
{
    public class FlightBookingViewModel : INotifyPropertyChanged
    {
        private readonly FlightBookingRespo _repo;

        public ObservableCollection<Table> FlightBookings { get; set; }

        private Table? _newBooking;
        public Table NewBooking
        {
            get => _newBooking;
            set
            {
                _newBooking = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }



        public FlightBookingViewModel()
        {
            _repo = new();
            FlightBookings = new();
            LoadFlightBookings();

            AddCommand = new RelayCommand(AddFlightingBooking);

            DeleteCommand = new RelayCommand(DeleteBook, CanDeleteBook);
        }

        private void LoadFlightBookings()
        {
            FlightBookings.Clear();
            var flightBookingList = _repo.GetAllFlightBooking();

            foreach (var book in flightBookingList)
            {
                FlightBookings.Add(book);
            }
        }

        private void AddFlightingBooking(object? obj)
        {
            if (string.IsNullOrWhiteSpace(NewBooking.PassengerName) ||
                string.IsNullOrWhiteSpace(NewBooking.FlightNumber) ||
                string.IsNullOrWhiteSpace(NewBooking.Departure) ||
                string.IsNullOrWhiteSpace(NewBooking.Destination) ||
                string.IsNullOrWhiteSpace(NewBooking.DepartureDate) ||
                string.IsNullOrWhiteSpace(NewBooking.DepartureTime) ||
                string.IsNullOrWhiteSpace(NewBooking.SeatClass))

            {
                MessageBox.Show("Please enter complete information!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _repo.Add(NewBooking);
            FlightBookings.Add(NewBooking); // Cập nhật UI ngay sau khi thêm sách
            MessageBox.Show("Added book successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteBook(object? obj)
        {
            if (SelectedBooking == null)
            {
                MessageBox.Show("Please select a book to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedBooking.PassengerName}'?",
                                         "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _repo.Delete(SelectedBooking);  // Xóa trong database
                FlightBookings.Remove(SelectedBooking);  // Xóa trên UI
                SelectedBooking = null;         // Reset SelectedBook
                MessageBox.Show("Book deleted successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private Table? _selectedBooking;
        public Table? SelectedBooking
        {
            get => _selectedBooking;
            set { _selectedBooking = value; OnPropertyChanged(); }
        }


        private bool CanDeleteBook(object? obj) => SelectedBooking != null;

        public event PropertyChangedEventHandler? PropertyChanged; // Cho phép null

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
