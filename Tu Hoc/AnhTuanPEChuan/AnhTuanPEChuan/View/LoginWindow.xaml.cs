using System.Windows;

namespace AnhTuanPEChuan.View
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.LoginViewModel();
        }
    }
}
