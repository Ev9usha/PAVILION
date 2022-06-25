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
using System.Diagnostics;

namespace PAVILION
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Imageget : Page
    {
        public Imageget()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var path2 = "C:\\Users\\evbod\\OneDrive\\Документы\\Учебная практика 6 семестр.летняя\\Image Сотрудники";
            var photos = Directory.EnumerateFiles(path2);
            using (PavilionEntities context = new PavilionEntities())
            {
                foreach (var photo in photos)
                {
                    string s = photo.Substring(photo.LastIndexOf('\\') + 1).Split(' ')[0];
                    var employ = context.Employeer.Where(x => x.surname == s).FirstOrDefault();
                    if (employ != null)
                        employ.foto = File.ReadAllBytes(photo);
                }
                context.SaveChanges();

                string path = "C:\\Users\\evbod\\OneDrive\\Документы\\Учебная практика 6 семестр.летняя\\Image ТЦ\\";
                var photos2 = Directory.EnumerateFiles(path);

                foreach (var photo in photos2)
                {
                    string s = photo.Substring(photo.LastIndexOf('\\') + 1);
                    string s2 = s.Substring(0, s.Length - 4);
                    var employ = context.TC.Where(x => x.name == s2).FirstOrDefault();
                    if (employ != null)
                        employ.picture = File.ReadAllBytes(photo);
                }
                context.SaveChanges();
            }
            MessageBox.Show("Попытка загрузки завершена");
        }
    }
}
