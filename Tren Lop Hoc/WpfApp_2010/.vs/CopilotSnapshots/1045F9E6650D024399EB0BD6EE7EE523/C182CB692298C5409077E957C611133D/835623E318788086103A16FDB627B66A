using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_2010
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            admin.admin adminWindow = new admin.admin();
            adminWindow.Show();
            this.Hide();
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            customer.customer customerWindow = new customer.customer();
            customerWindow.Show();
            this.Hide();
        }
    }
}