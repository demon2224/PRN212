using AnhTuanPEChuan.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WatchPRN212.Helper;

namespace AnhTuanPEChuan.ViewModel
{
    public class WatchViewModel : BaseViewModel
    {
        public ObservableCollection<Watch> Watches { get; set; } = new();
        public ObservableCollection<Brand> Brands { get; set; } = new();

        private Watch _selectedWatch;
        public Watch SelectedWatch
        {
            get => _selectedWatch;
            set { _selectedWatch = value; OnPropertyChanged(); }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                Search(); // chạy mỗi khi gõ
            }
        }

        public Member CurrentUser { get; }

        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public WatchViewModel(Member user)
        {
            CurrentUser = user;
            LoadData();
            AddCommand = new RelayCommand(AddWatch);
            UpdateCommand = new RelayCommand(UpdateWatch);
            DeleteCommand = new RelayCommand(DeleteWatch);
        }

        // 🔹 Load dữ liệu ban đầu
        private void LoadData()
        {
            using var context = new WatchDb2024Context();
            Watches = new ObservableCollection<Watch>(
                context.Watches
                       .Include(w => w.Brand) // ✅ load luôn Brand
                       .ToList()
            );

            Brands = new ObservableCollection<Brand>(context.Brands.ToList());
            OnPropertyChanged(nameof(Watches));
        }


        // 🔹 Search realtime
        private void Search()
        {
            using var context = new WatchDb2024Context();
            string keyword = (SearchText ?? "").Trim().ToLower();

            var filtered = string.IsNullOrWhiteSpace(keyword)
                ? context.Watches.Include(w => w.Brand).ToList()
                : context.Watches.Include(w => w.Brand)
                         .Where(w => w.WatchName.ToLower().Contains(keyword)
                                  || w.Description.ToLower().Contains(keyword))
                         .ToList();

            Watches.Clear();
            foreach (var w in filtered)
                Watches.Add(w);
        }


        // 🔹 Add
        private void AddWatch(object obj)
        {
            if (CurrentUser.Role < 2)
            {
                MessageBox.Show("No permission");
                return;
            }

            // Lấy hàng đang được nhập trong DataGrid
            var newWatch = SelectedWatch;

            if (newWatch == null ||
                string.IsNullOrWhiteSpace(newWatch.WatchName) ||
                newWatch.Price < 0 || newWatch.Price > 1000)
            {
                MessageBox.Show("Invalid data");
                return;
            }

            using var context = new WatchDb2024Context();
            context.Watches.Add(newWatch);
            context.SaveChanges();

            // ⚠️ Không thêm lại vào Watches (vì DataGrid đã tự có nó)
            MessageBox.Show("Added successfully!");

            // Làm mới để hiển thị ID thật từ DB
            LoadData();
        }


        // 🔹 Update
        private void UpdateWatch(object obj)
        {
            if (CurrentUser.Role < 2)
            {
                MessageBox.Show("No permission");
                return;
            }

            if (SelectedWatch == null)
            {
                MessageBox.Show("Please select a watch to update");
                return;
            }

            using var context = new WatchDb2024Context();
            var watch = context.Watches.Find(SelectedWatch.WatchId);
            if (watch == null) return;

            watch.WatchName = SelectedWatch.WatchName;
            watch.Price = SelectedWatch.Price;
            watch.Description = SelectedWatch.Description;
            watch.BrandId = SelectedWatch.BrandId;
            context.SaveChanges();

            MessageBox.Show("Updated successfully!");
        }

        // 🔹 Delete
        private void DeleteWatch(object obj)
        {
            if (CurrentUser.Role < 2)
            {
                MessageBox.Show("No permission");
                return;
            }

            if (SelectedWatch == null)
            {
                MessageBox.Show("Please select a watch to delete");
                return;
            }

            using var context = new WatchDb2024Context();
            var watch = context.Watches.Find(SelectedWatch.WatchId);
            if (watch != null)
            {
                if (MessageBox.Show($"Delete '{watch.WatchName}' ?", "Confirm", MessageBoxButton.YesNo)
                    == MessageBoxResult.Yes)
                {
                    context.Watches.Remove(watch);
                    context.SaveChanges();

                    // ✅ Xóa trực tiếp khỏi ObservableCollection (UI update ngay)
                    Watches.Remove(SelectedWatch);
                    SelectedWatch = null;

                    MessageBox.Show("Deleted successfully!");
                }
            }
        }
    }
}
