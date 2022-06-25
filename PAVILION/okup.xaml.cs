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

    public class ResultFor
    {
        public string name { get; set; }

        public string city { get; set; }

        public decimal Okup { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для okup.xaml
    /// </summary>
    public partial class okup : Page
    {
        public okup()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PavilionEntities bd = new PavilionEntities();
            OkDataGrid.ItemsSource = bd.Database.SqlQuery<ResultFor>("dbo.OKUP").ToList();
            OkDataGrid.Columns[0].Width = 200;
            OkDataGrid.Columns[1].Width = 200;
            OkDataGrid.Columns[2].Width = 200;
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new main_page());
        }
    }
}
