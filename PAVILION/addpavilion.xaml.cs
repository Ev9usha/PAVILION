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
    /// Логика взаимодействия для addpavilion.xaml
    /// </summary>
    public partial class addpavilion : Page
    {

        public addpavilion()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var db = new PavilionEntities();
            var statuss = db.Pavilions.Select(s => s.status).Distinct().ToList();
            SelectSTP.ItemsSource = statuss;
            this.DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var errors = new StringBuilder();
            if (string.IsNullOrEmpty(floor1.Text))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrEmpty(pav2.Text))
                errors.AppendLine("Укажите коэффициент");
            if (string.IsNullOrEmpty(plosh3.Text))
                errors.AppendLine("Укажите затраты на постройку");
            if (string.IsNullOrEmpty(SelectSTP.Text))
                errors.AppendLine("Укажите город");
            if (string.IsNullOrEmpty(coef5.Text))
                errors.AppendLine("Укажите этажи");
            if (string.IsNullOrEmpty(cost6.Text))
                errors.AppendLine("Укажите количество павильонов");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                var entities = new PavilionEntities();
                var Pavilions = new Pavilions
                {
                    floar = Convert.ToInt32(floor1.Text),
                    num_pavilion = pav2.Text,
                    s = Convert.ToDecimal(plosh3.Text),
                    status = SelectSTP.Text,
                    coefficient = Convert.ToDouble(coef5.Text),
                    money = Convert.ToDecimal(cost6.Text)
                };
                entities.Pavilions.Add(Pavilions);
                entities.SaveChanges();
                MessageBox.Show(string.Format("Успешно добавлено  ({0})", pav2.Text));

            }
            catch (Exception ex) { 
                MessageBox.Show(ex.ToString());
            }


        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new pavilion_page(14));
        }
    }
}
