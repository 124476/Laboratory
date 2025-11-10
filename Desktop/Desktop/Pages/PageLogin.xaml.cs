using Desktop.Models;
using Desktop.Properties;
using Desktop.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        bool IsVisible = true;
        string Password = "";
        int CountLogin = 0;

        DispatcherTimer timer = new DispatcherTimer();

        public PageLogin()
        {
            InitializeComponent();

            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(10);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            BtnOpen.IsEnabled = true;
            timer.Stop();
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            var login = TextLogin.Text;

            if (IsVisible) Password = TextPassword.Text;
            else Password = PasswordPassword.Password;

            App.User = App.DB.User.FirstOrDefault(x => x.Login == login && x.Password == Password);
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.DataContext = App.User;

            if (App.User == null)
            {
                MessageBox.Show("Неверные данные");

                bool result = (new WindowCaptcha()).ShowDialog().GetValueOrDefault();
                if (!result)
                {
                    BtnOpen.IsEnabled = false;
                    timer.Start();
                }

                return;
            }

            if (App.User.Id == Settings.Default.UserId && (DateTime.Now - Settings.Default.DateLast).Minutes < 1)
            {
                MessageBox.Show("Проветривание");
                return;
            }

            if (App.User.RoleId == 1) MessageBox.Show("В дальнейшем...");
            if (App.User.RoleId == 2) NavigationService.Navigate(new PageAdmin());
            if (App.User.RoleId == 3) NavigationService.Navigate(new PageAccountant());
            if (App.User.RoleId == 4) NavigationService.Navigate(new PageLaboratory());
            if (App.User.RoleId == 5) NavigationService.Navigate(new PageLaboratoryResearcher());
            
            App.DB.LoggingUser.Add(new LoggingUser()
            {
                User = App.User,
                DateTime = DateTime.Now,
            });
            App.DB.SaveChanges();
        }

        private void BtnSetVisible_Click(object sender, RoutedEventArgs e)
        {
            IsVisible = !IsVisible;

            if (IsVisible)
            {
                Password = PasswordPassword.Password;
                PasswordPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                Password = TextPassword.Text;
                PasswordPassword.Visibility = Visibility.Visible;
            }

            PasswordPassword.Password = Password;
            TextPassword.Text = Password;
        }
    }
}
