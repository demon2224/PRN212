using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace LAB2710.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        [RelayCommand]
        private void GoToManagePage()
        {
            var manageWindow = new Views.ManagePage();
            manageWindow.Show();
            
            Application.Current.MainWindow?.Close();
        }
    }
}