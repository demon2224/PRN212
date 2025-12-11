using AnhTuanPEChuan.Models;
using AnhTuanPEChuan.View;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WatchPRN212.Helper;

namespace AnhTuanPEChuan.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object obj)
        {
            // ✅ nhận PasswordBox từ CommandParameter và lấy Password
            var pwBox = obj as PasswordBox;
            var password = pwBox?.Password ?? string.Empty;

            using var context = new WatchDb2024Context();
            var user = context.Members.FirstOrDefault(u => u.Email == Email && u.Password == password);

            if (user == null)
            {
                MessageBox.Show("Invalid email or password");
                return;
            }

            if (user.Role == 2 || user.Role == 3)
            {
                var window = new WatchWindow(user);
                window.Show();
                Application.Current.Windows[0]?.Close();
            }
            else
            {
                MessageBox.Show("You have no permission to access this function");
            }
        }
    }
}
