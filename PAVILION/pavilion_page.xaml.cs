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
    /// Логика взаимодействия для pavilion_page.xaml
    /// </summary>
    public partial class pavilion_page : Page
    {
        public Pavilions SelectedPv { get; set; }
        public static int TCID { get; set; }

        public pavilion_page(int TC_ID)
        {
            TCID = TC_ID;
            InitializeComponent();
            Showed();
        }


        private void Showed()
        {
            var db = new PavilionEntities();
            PavGrid.ItemsSource = db.TC.Where(s => s.id_tc == TCID).ToList();
            PavGrid.ItemsSource = db.Pavilions.Where(s => s.id_tc == TCID).ToList();
            //var db = new PavilionEntities();
            ////Pavilions id = db.Pavilions.Select().Where(st => st.id_tc == TCID).FirstorDefault();

            ////PavGrid.ItemsSource = id;

            // db = new PavilionEntities();
            //var result = (
            //    from center in db.TC
            //        //  where id_tc == TCID
            //    select center).ToList();
            //PavGrid.ItemsSource = result;
        }


        private void Page_Loaded1(object sender, RoutedEventArgs e)
        {
            var db = new PavilionEntities();
            var statuss = db.Pavilions.Select(s => s.status).Distinct().ToList();
            Combo1.ItemsSource = statuss;
            this.DataContext = this;
        }
        private void Page_Loaded2(object sender, RoutedEventArgs e)
        {
            var db = new PavilionEntities();
            var ffloors = db.Pavilions.Select(s => s.floar).Distinct().ToList();
            Combo2.ItemsSource = ffloors;
            this.DataContext = this;
        }
        private void Page_Loaded3(object sender, RoutedEventArgs e)
        {
            var db = new PavilionEntities();
            var sss = db.Pavilions.Select(s => s.s).Distinct().ToList();
            Combo3.ItemsSource = sss;
            
        }

        private string _sort1;
        public string SortStatus
        {
            get => _sort1;
            set
            {
                PavilionEntities db = new PavilionEntities();
                var pav = db.Pavilions.ToList();
                _sort1 = value;

                if (_sort1 != null)

                    PavGrid.ItemsSource = db.Pavilions.Where(s => s.status == SortStatus && s.id_tc == TCID).ToList();
                else
                    PavGrid.ItemsSource = db.Pavilions.Where(s => s.status == SortStatus && s.status == SortFloor && s.status == SortPloshad && s.id_tc == TCID).ToList();

            }
        }

        private string _sort2;
        public string SortFloor
        {
            get => _sort2;
            set
            {
                PavilionEntities db = new PavilionEntities();
                var pav = db.Pavilions.ToList();
                _sort2 = value;

                if (_sort2 != null)
                    PavGrid.ItemsSource = db.Pavilions.Where(p => p.status == SortFloor).ToList();
                else
                    PavGrid.ItemsSource = db.Pavilions.Where(p => p.status == SortStatus && p.status == SortPloshad && p.status == SortStatus).ToList();
            }
        }


        private string _sort3;
        public string SortPloshad
        {
            get => _sort3;
            set
            {
                PavilionEntities db = new PavilionEntities();
                var pav = db.Pavilions.ToList();
                _sort3 = value;
                if (_sort2 != null)
                    PavGrid.ItemsSource = db.Pavilions.Where(p => p.status == SortPloshad).ToList();
                else
                    PavGrid.ItemsSource = db.Pavilions.Where(p => p.status == SortPloshad && p.status == SortStatus && p.status == SortFloor).ToList();
            }
        }

        public void OnDeletePavExecute()
        {
            using (PavilionEntities db = new PavilionEntities())
            {
                Pavilions statuss = db.Pavilions.Where(st => st.num_pavilion == SelectedPv.num_pavilion).FirstOrDefault();
                if (statuss != null)
                {
                    statuss.status = "Удален";
                    db.SaveChanges();
                    PavGrid.ItemsSource = db.Pavilions.ToList();
                }

            }
        }

        private void DEl_Click(object sender, RoutedEventArgs e)
        {
            OnDeletePavExecute();
        }

        private void AddPav(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new addpavilion());
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new main_page());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
