using PE_CE180905_LeAnhTuan.ViewModel;
using System.Windows;

namespace PE_CE180905_LeAnhTuan.View
{
    /// <summary>
    /// Interaction logic for BookStoreManagement.xaml
    /// </summary>
    public partial class BookStoreManagement : Window
    {
        public BookStoreManagement()
        {
            InitializeComponent();
            this.DataContext = new BookStoreViewModel();
        }
    }
}
