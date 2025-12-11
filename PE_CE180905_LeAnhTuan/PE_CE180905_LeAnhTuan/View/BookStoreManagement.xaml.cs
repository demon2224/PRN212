using PE_CE180905_LeAnhTuan.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
