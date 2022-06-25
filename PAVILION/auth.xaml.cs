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

namespace PAVILION
{
    /// <summary>
    /// Логика взаимодействия для auth.xaml
    /// </summary>
    public partial class auth : Page
    {
        public auth()
        {
            InitializeComponent();
        }

        int c = 0;
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            using (PavilionEntities cnt = new PavilionEntities())
            {
                bool success = false;
                if (Login.Text != string.Empty && Password.Password != string.Empty)
                {
                    if (cnt.Employeer.Where(p => p.login == Login.Text).Count() < 1)
                        errors.AppendLine("Логин не верный");
                    if (cnt.Employeer.Where(p => p.login == Login.Text && p.password == Password.Password).Count() < 1)
                        errors.AppendLine("Некорректный пароль");
                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                    }
                    else
                    {
                        success = true;
                    }
                }
                if (success) { NavigationService.Navigate(new main_page()); }
                else
                {
                    Login.Clear();
                    Password.Clear();
                    Login.Text = "Логин";
                    c++;
                }
                if(c >= 3)
                {
                    Captcha.Visibility = Visibility.Visible;
                    Captcha_Check.Visibility = Visibility.Visible;
                    char s;
                    int[] cifra = new int[8];
                    char[] bukva = new char[8] { 'A', 'B', 'C', 'D', 'E', 'R', 'J', 'Q' };
                    Random rnd = new Random();
                    for (int i = 0; i <= 7; i++)
                    {
                        cifra[i] = rnd.Next(10);
                        s = bukva[rnd.Next(7)];
                        Captcha.Text = (cifra[i].ToString() + s + cifra[i].ToString() + cifra[i].ToString() + s);
                        //Captcha.FontWeight = "200";
                    }
                    if (Login.Text != string.Empty && Password.Password != string.Empty && Captcha.Text == Captcha_Check.Text)
                    {
                        NavigationService.Navigate(new main_page());
                    }
     
                        if (Login.Text != string.Empty && Password.Password != string.Empty && Captcha.Text != Captcha_Check.Text)
                        {
                            MessageBox.Show("ОБНАРУЖЕН РОБОТ! ДОСТУП ОГРАНИЧЕН"); Close();
                        }
                    
                    Captcha_Check.Clear();
                }
            }

        }

        private void Close()
        {
            throw new NotImplementedException();
        }
    }
}
