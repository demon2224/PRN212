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
using WPF_Tu_Hoc.Entities;
using WPF_Tu_Hoc.Services;

namespace WPF_Tu_Hoc.View
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private SachService _service = new();
        
        public DetailWindow()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Sach x = new();
            x.Id = txtID.Text;
            x.TenSach = txtTenSach.Text;
            x.GiaBan = txtGiaBan.Text;
            x.NamXuatBan = int.Parse(txtNamXuatBan.Text);

            _service.AddSach(x);
            this.Close();
        }
    }
}
