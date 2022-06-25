using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace PAVILION
{
    /// <summary>
    /// Логика взаимодействия для main_page.xaml
    /// </summary>
    public partial class main_page : Page
    {
        public TC SelectedTC { get; set; }
        public static string TCname { get; set; }
        public static string TCcoef { get; set; }
        public static string TCfl { get; set; }
        public static string TCmoney { get; set; }
        public static string TCcity { get; set; }
        public static string TCcol { get; set; }


        public main_page()
        {
            InitializeComponent();
            Showed();
        }

        private void AddTC(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new addcenter());
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new auth());
        }

        private void PavilionList(object sender, RoutedEventArgs e)
        {
             using (PavilionEntities db = new PavilionEntities())
            {
                TC tc = TCGrid.SelectedItem as TC;
                int id = tc.id_tc;

                Pavilions statuss = db.Pavilions.Where(st => st.id_tc == SelectedTC.id_tc).FirstOrDefault();
                if (statuss != null)
                {
                    NavigationService.Navigate(new pavilion_page(id));
                }

             }


        }

        private void DataGridMouseEventClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var row = e.Source as DataGridRow;
                dynamic puff = TCGrid.SelectedItem;
                if (puff != null)
                {
                    TCname = puff.name;
                    TCcoef = Convert.ToString(puff.coefficient);
                    TCmoney = Convert.ToString(puff.money);
                    TCfl = Convert.ToString(puff.floors);
                    TCcity = puff.city;
                    TCcol = Convert.ToString(puff.colichestvo);
                }
                
                NavigationService.Navigate(new addcenter());

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var db = new PavilionEntities();
            var cyties = db.TC.Select(s => s.city).Distinct().ToList();
            CityChan.ItemsSource = cyties;
          //  TCGrid.DataContext = this;
            this.DataContext = this;
        }
        private void Page_Loaded2(object sender, RoutedEventArgs e)
        {
            var db = new PavilionEntities();
            var statuss = db.TC.Select(s => s.status).Distinct().ToList();
            StatusChan.ItemsSource = statuss;

            this.DataContext = this;
        }
  
        private void Showed()
        {
            var db = new PavilionEntities();
            var result = (
                from center in db.TC
                select center).ToList();
            TCGrid.ItemsSource = result;
        }

        public void OnDeleteShopExecute()
        {
            using (PavilionEntities db = new PavilionEntities())
            {
                TC statuss = db.TC.Where(st => st.id_tc == SelectedTC.id_tc).FirstOrDefault();
                if (statuss != null)
                {
                    statuss.status = "Удален";
                    db.SaveChanges();
                    TCGrid.ItemsSource = db.TC.ToList();
                }

            }
        }

        private string _sort;
        public string SortStatus
        {
            get => _sort;
            set
            {
                PavilionEntities db = new PavilionEntities();
                var tc = db.TC.ToList();
                _sort = value;

               if (_sort != null)
               
                    TCGrid.ItemsSource = db.TC.Where(s => s.status == SortStatus).ToList();
              else
                        TCGrid.ItemsSource = db.TC.Where(s => s.status == SortStatus && s.status == CitySort).ToList();

            }
        }

        private string _citysort;
        public string CitySort
        {
            get => _citysort;
            set
            {
                PavilionEntities db = new PavilionEntities();
                var tc = db.TC.ToList();
                _citysort = value;

                if (_sort != null) 
                    TCGrid.ItemsSource = db.TC.Where(s => s.status == CitySort).ToList();
                else
                    TCGrid.ItemsSource = db.TC.Where(s => s.status == SortStatus && s.status == CitySort).ToList();
            }
        }

        private void CityChan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StatusChan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DEl_Click(object sender, RoutedEventArgs e)
        {
            OnDeleteShopExecute();
        }
    }
}
