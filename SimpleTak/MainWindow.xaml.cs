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
using SimpleTak.Pages;
using SimpleTak.Model;

namespace SimpleTak
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static testCyberEntities Context = new testCyberEntities();
        public MainWindow()
        {
            InitializeComponent();
            navigationFrame.Navigate(new Page1());
        }
    }
}
