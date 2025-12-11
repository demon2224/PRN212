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
using WPF_Tu_Hoc.Entities;
using WPF_Tu_Hoc.Services;
using WPF_Tu_Hoc.View;

namespace WPF_Tu_Hoc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SachService _service = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DetailWindow detail = new DetailWindow();
            detail.ShowDialog();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            List<Sach> saches = null;
            saches = _service.GetAllSach();
            dtgSach.ItemsSource = saches;
        }
    }
}