using Desktop.Models;
using Desktop.Pages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Desktop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() 
        {
            InitializeComponent();

            MyFrame.Navigate(new PageLogin());
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            App.User = null;
            MyFrame.Navigate(new PageLogin());
        }

        private void MyFrame_ContentRendered(object sender, EventArgs e)
        {
            if (App.User != null) StackHeader.Visibility = Visibility.Visible; 
            else StackHeader.Visibility = Visibility.Collapsed;
        }
    }
}
