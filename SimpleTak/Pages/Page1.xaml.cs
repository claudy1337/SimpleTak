using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using SimpleTak.Model;

namespace SimpleTak.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            init();
            ShowMessageBasedOnTime();
        }

        private void tbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var getDate = datePicker.SelectedDate;
            var selectType = tbType.SelectedItem as Role;
            if (getDate == null)
            {
                lstv.ItemsSource = DBConnection.connection.User.Where(t => t.RoleId == selectType.id).ToList();
            }
            else
            {
                lstv.ItemsSource = DBConnection.connection.User.Where(t => t.RoleId == selectType.id && t.DateBirthDay == getDate).ToList();
            }
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.MinValue;
            FilterListViewByDate(lstv, selectedDate);
        }
        private void FilterListViewByDate(ListView listView, DateTime filterDate)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(listView.ItemsSource);
            view.Filter = item =>
            {
                if (item is User yourItem)
                {
                    return yourItem.DateBirthDay == filterDate.Date;
                }
                return false;
            };
            view.Refresh();
        }
        private void init()
        {
            lstv.ItemsSource = DBConnection.connection.User.ToList();
            tbType.ItemsSource = DBConnection.connection.Role.ToList();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            init();
            NavigationService.Navigate(new Page1());
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            var getUser = DBConnection.connection.User.FirstOrDefault(u => u.Login == tbLogin.Text && u.Password == pbPassword.Password);
            if (getUser != null)
            {
                MessageBox.Show(getUser.Name);
            }
        }

        private void ShowMessageBasedOnTime()
        {
            DateTime currentTime = DateTime.Now;
            string message;

            if (currentTime.Hour < 12)
            {
                message = "Доброе утро!";
            }
            else if (currentTime.Hour < 18)
            {
                message = "Добрый день!";
            }
            else
            {
                message = "Добрый вечер!";
            }
            timeHi.Text = message;
        }

        private void Btngenerate_Click(object sender, RoutedEventArgs e)
        {
            txtGenerate.Text = RandomLogin().ToString();
            MessageBox.Show($"{ValidateTextbox(validation.Text)}");
        }
        private int RandomLogin()
        {
            Random rnd = new Random();
            var randomBetween = rnd.Next(12345, 99999);
            var dontLogin = DBConnection.connection.User.FirstOrDefault(l => l.Password == randomBetween.ToString());
            if (dontLogin == null)
                return randomBetween;
            else
                return RandomLogin();
        }
        public bool ValidateTextbox(string text)
        {
            if (text.Length < 6)
                return false;

            if (!text.Any(char.IsUpper))
                return false;

            if (!text.Any(char.IsLower))
                return false;

            if (!text.Any(char.IsDigit))
                return false;

            if (!Regex.IsMatch(text, @"[!@#$%^&*(),.?\:{ }|<>]"))
                return false;

            return true;
        }

        private int? getUserRole(string login)
        {
            var getRole = DBConnection.connection.User.FirstOrDefault(l => l.Login == login);
            return getRole?.RoleId;
        }
        private bool getLogin(string login)
        {
            var getLogin = DBConnection.connection.User.FirstOrDefault(l => l.Login == login);
            return getLogin != null ? true : false;
        }

        private void lstv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var select = lstv.SelectedItem as User;
            if (select != null )
            {
                MessageBox.Show(select.Name);
            }
        }
    }
}
