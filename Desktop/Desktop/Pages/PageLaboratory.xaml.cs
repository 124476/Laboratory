using Desktop.Properties;
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
using System.Windows.Threading;

namespace Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageLaboratory.xaml
    /// </summary>
    public partial class PageLaboratory : Page
    {
        DateTime Time = new DateTime(1, 1, 1, 0, 2, 0);
        DispatcherTimer timer = new DispatcherTimer();
        public PageLaboratory()
        {
            InitializeComponent();

            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Time -= TimeSpan.FromSeconds(1);
            TextTimer.Text = $"{Time.Minute:00}:{Time.Second:00}";

            if (Time.Minute == 1 && Time.Second == 0)
            {
                MessageBox.Show("Скоро конец сессии", "Сообщение");
                return;
            }

            if (Time.Minute == 0 && Time.Second == 0)
            {
                Settings.Default.UserId = App.User.Id;
                Settings.Default.DateLast = DateTime.Now;
                Settings.Default.Save();

                timer.Stop();

                App.User = null;
                NavigationService.Navigate(new PageLogin());
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }
    }
}
