using Microsoft.Win32;
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
    /// Логика взаимодействия для addcenter.xaml
    /// </summary>
    public partial class addcenter : Page
    {
        public static string IDpav { get; set; }

        public addcenter()
        {
            InitializeComponent();
            fillInfo();
        }
        public void fillInfo()
        {
            nm.Text = main_page.TCname;
            cf.Text = main_page.TCcoef;
            fl.Text = main_page.TCfl;
            zatr.Text = main_page.TCmoney;
            city.Text = main_page.TCcity;
            cl.Text = main_page.TCcol;
        }

        private void StatusAdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var db = new PavilionEntities();
            var statuss = db.TC.Select(s => s.status).Distinct().ToList();
            StatusAdd.ItemsSource = statuss;
            this.DataContext = this;
        }




        private void PavListButton_Click(object sender, RoutedEventArgs e)
        {
            var db = new PavilionEntities();
            ////SqlCommand ID_Pav_Command = new SqlCommand("SELECT dbo.Pavilions.ID_pavilion FROM dbo.ShoppingCenter INNER JOIN dbo.Pavilions ON dbo.ShoppingCenter.ID_shoppingCenter = dbo.Pavilions.ID_shoppingCenter WHERE(dbo.ShoppingCenter.ShoppingCenter_name = @name)", con);


            ////db.Parameters.AddWithValue("@name", ChangeName.Text);
            //SqlDataReader sqlDataReaderPav = ID_Pav_Command.ExecuteReader();


            //while (sqlDataReaderPav.Read())
            //{

            //    ID_Pavilion = sqlDataReaderPav.GetValue(0).ToString();

            //}
            //sqlDataReaderPav.Close();



            //PavilionEntities pavilionsList = new PavilionEntities();

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new main_page());
        }

        private byte[] BitmapSourceToByteArray(BitmapSource image)
        {
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder(); // or some other encoder
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            var errors = new StringBuilder();
            if (string.IsNullOrEmpty(nm.Text))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrEmpty(cf.Text))
                errors.AppendLine("Укажите коэффициент");
            if (string.IsNullOrEmpty(zatr.Text))
                errors.AppendLine("Укажите затраты на постройку");
            if (string.IsNullOrEmpty(city.Text))
                errors.AppendLine("Укажите город");
            if (string.IsNullOrEmpty(fl.Text))
                errors.AppendLine("Укажите этажи");
            if (string.IsNullOrEmpty(cl.Text))
                errors.AppendLine("Укажите количество павильонов");




            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                var entities = new PavilionEntities();
                if (!(string.IsNullOrEmpty(nm.Text)) && nm.Text.GetType() != typeof(string))
                {
                    MessageBox.Show("Должно быть строка");
                }
                else if (!(string.IsNullOrEmpty(cl.Text)) && cl.Text.GetType() != typeof(int))
                {
                    MessageBox.Show("Должны быть числа");
                }
                else if (!(string.IsNullOrEmpty(fl.Text)) && cl.Text.GetType() != typeof(int))
                {
                    MessageBox.Show("Должны быть числа");
                }
                else if (!(string.IsNullOrEmpty(zatr.Text)) && cl.Text.GetType() != typeof(decimal))
                {
                    MessageBox.Show("Должны быть числа");
                }
                else if (!(string.IsNullOrEmpty(cf.Text)) && cl.Text.GetType() != typeof(decimal))
                {
                    MessageBox.Show("Должны быть числа");
                }
                else if (!(string.IsNullOrEmpty(city.Text)) && cl.Text.GetType() != typeof(string))
                {
                    MessageBox.Show("Должна быть строка");

                }
                else
                {

                    var TC = new TC
                    {
                        name = nm.Text,
                        coefficient = Convert.ToDouble(cl.Text),
                        floors = Convert.ToInt32(fl.Text),
                        money = Convert.ToDecimal(zatr.Text),
                        colichestvo = Convert.ToInt32(cl.Text),
                        status = StatusAdd.Text,
                        city = city.Text,
                        picture = BitmapSourceToByteArray((BitmapSource)Izobrajenie.Source)

                    };
                    entities.TC.Add(TC);
                    entities.SaveChanges();
                    MessageBox.Show(string.Format("Успешно добавлено  ({0})", nm.Text));//??

                }
            }catch (Exception ex) { }
               
                


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
    }
}
