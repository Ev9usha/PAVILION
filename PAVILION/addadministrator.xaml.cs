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
using System.IO;

namespace PAVILION
{
    /// <summary>
    /// Логика взаимодействия для addadministrator.xaml
    /// </summary>
    public partial class addadministrator : Page
    {
        public addadministrator()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new main_page());
        }

        private void iz_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = ""; // Default file name
                               // dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
         "Portable Network Graphic (*.png)|*.png";   // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open documentl
                Izobrajenie.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var errors = new StringBuilder();
            if (string.IsNullOrEmpty(TextBox1.Text))
                errors.AppendLine("Укажите фамилию");
            if (string.IsNullOrEmpty(TextBox2.Text))
                errors.AppendLine("Укажите имя");
            if (string.IsNullOrEmpty(TextBox3.Text))
                errors.AppendLine("Укажите отчество");
            if (string.IsNullOrEmpty(TextBox4.Text))
                errors.AppendLine("Укажите логин");
            if (string.IsNullOrEmpty(TextBox5.Text))
                errors.AppendLine("Укажите пароль");
            if (string.IsNullOrEmpty(TextBox6.Text))
                errors.AppendLine("Укажите пол");
            if (string.IsNullOrEmpty(TextBox7.Text))
                errors.AppendLine("Укажите роль");
            if (string.IsNullOrEmpty(TextBox8.Text))
                errors.AppendLine("Укажите номер телефона");



            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                var entities = new PavilionEntities();
                var Employeer = new Employeer
                {
                    surname = TextBox1.Text,
                    name = TextBox2.Text,
                    midllename = TextBox3.Text,
                    login = TextBox4.Text,
                    password = TextBox5.Text,
                    gender = TextBox6.Text,
                    role = TextBox7.Text,
                    phone = TextBox8.Text

                };
                entities.Employeer.Add(Employeer);
                entities.SaveChanges();
                MessageBox.Show(string.Format("Успешно добавлено  ({0} {1} {2})", TextBox1.Text, TextBox2.Text, TextBox3.Text));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
