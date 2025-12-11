using System.Windows;
using AnhTuanPEChuan.Models;
using AnhTuanPEChuan.ViewModel;

namespace AnhTuanPEChuan.View
{
    public partial class WatchWindow : Window
    {
        public WatchWindow(Member user)
        {
            InitializeComponent();
            DataContext = new WatchViewModel(user);
        }
    }
}
