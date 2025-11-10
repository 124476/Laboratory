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
using System.Windows.Threading;

namespace Desktop.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowCaptcha.xaml
    /// </summary>
    public partial class WindowCaptcha : Window
    {
        int FirstX = 0;
        int LastX = 0;
        DispatcherTimer timer = new DispatcherTimer();
        public WindowCaptcha()
        {
            InitializeComponent();

            LastX = 5 * (new Random()).Next(0, 80);
            Canvas.SetLeft(ImgLast, LastX);

            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (FirstX - 75 == LastX)
            {
                timer.Stop();
                DialogResult = true;
            }
        }

        private void SliderCaptcha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FirstX = 5 * (int)SliderCaptcha.Value;
            Canvas.SetLeft(ImgFirst, FirstX);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            timer.Stop();
        }
    }
}
