using PETest1.ViewModel;
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

namespace PETest1.View
{
    /// <summary>
    /// Interaction logic for FlightBookingWindow.xaml
    /// </summary>
    public partial class FlightBookingWindow : Window
    {
        public FlightBookingWindow()
        {
            
            InitializeComponent();
            this.DataContext = new FlightBookingViewModel();
        }
    }
}
