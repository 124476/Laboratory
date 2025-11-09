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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        bool IsVisible = true;
        string password = "";

        public PageLogin()
        {
            InitializeComponent();
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            var login = TextLogin.Text;

            if (IsVisible) password = TextPassword.Text;
            else password = PasswordPassword.Password;

            App.User = App.DB.User.FirstOrDefault(x => x.Login == login && x.Password == password);
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.DataContext = App.User;

            if (App.User == null)
            {
                MessageBox.Show("Неверные данные");
                return;
            }

            if (App.User.RoleId == 1) MessageBox.Show("В дальнейшем...");
            if (App.User.RoleId == 2) NavigationService.Navigate(new PageAdmin());
            if (App.User.RoleId == 3) NavigationService.Navigate(new PageAccountant());
            if (App.User.RoleId == 4) NavigationService.Navigate(new PageLaboratory());
            if (App.User.RoleId == 5) NavigationService.Navigate(new PageLaboratoryResearcher());
        }

        private void BtnSetVisible_Click(object sender, RoutedEventArgs e)
        {
            IsVisible = !IsVisible;

            if (IsVisible)
            {
                password = PasswordPassword.Password;
                PasswordPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                password = TextPassword.Text;
                PasswordPassword.Visibility = Visibility.Visible;
            }

            PasswordPassword.Password = password;
            TextPassword.Text = password;
        }
    }
}
