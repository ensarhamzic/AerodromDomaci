using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace AerodromDomaci
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DB baza;
        public Avion Avion { get; set; }
        public ObservableCollection<Avion> Avioni { get; set; }
        public MainWindow()
        {
            baza = new DB();
            Avion = new Avion();
            UcitajAvione();
            InitializeComponent();
        }

        private void UcitajAvione()
        {
            DataTable dt = baza.Read() as DataTable;
            Avioni = new ObservableCollection<Avion>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Avion temp = new Avion
                {
                    Id = Convert.ToInt32(dt.Rows[i]["id"]),
                    Tip = (string)dt.Rows[i]["ime_tipa"],
                    SerijskiBroj = Convert.ToInt32(dt.Rows[i]["ser_br"]),
                    RegistracioniBroj = Convert.ToInt32(dt.Rows[i]["reg_br"]),
                    Vlasnik = new Vlasnik(dt.Rows[i]["ime"].ToString(), dt.Rows[i]["prezime"].ToString()),
                    BrojSedista = Convert.ToInt32(dt.Rows[i]["br_sedista"]),
                    KapacitetRezervoara = Convert.ToInt32(dt.Rows[i]["kapacitet_rez"]),
                    Nosivost = Convert.ToInt32(dt.Rows[i]["nosivost"]),
                    
                };
                if(dt.Rows[i]["broj_raketa"] is DBNull)
                {
                    temp.BrojRaketa = null;
                }
                else
                {
                    temp.BrojRaketa = Convert.ToInt32(dt.Rows[i]["broj_raketa"]);
                }
                Avioni.Add(temp);
            };
        }

        private void DodajAvion(object sender, RoutedEventArgs e)
        {
            // TODO:  VALIDACIJA
            var result = baza.Create(Avion);
            if (result)
            {
                Avioni.Add(new Avion
                {
                    Id = Avion.Id,
                    Tip = Avion.Tip,
                    SerijskiBroj = Avion.SerijskiBroj,
                    RegistracioniBroj = Avion.RegistracioniBroj,
                    Vlasnik = Avion.Vlasnik,
                    BrojSedista = Avion.BrojSedista,
                    KapacitetRezervoara = Avion.KapacitetRezervoara,
                    Nosivost = Avion.Nosivost
                });
            }
            else
            {
                MessageBox.Show("Doslo je do greske!");
            }
            Avion.SerijskiBroj = 0;
            Avion.RegistracioniBroj = 0;
            Avion.Vlasnik = new Vlasnik();
            Avion.BrojSedista = 0;
            Avion.KapacitetRezervoara = 0;
            Avion.Nosivost = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            ComboBoxItem cbi = (ComboBoxItem)combo.SelectedItem;
            Avion.Tip = cbi.Content.ToString();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var selitem = dataGrid.SelectedItem as Avion;
            Avion.Id = selitem.Id;
            Avion.Tip = selitem.Tip;
            Avion.SerijskiBroj = selitem.SerijskiBroj;
            Avion.RegistracioniBroj = selitem.RegistracioniBroj;
            Avion.Vlasnik.Ime = selitem.Vlasnik.Ime;
            Avion.Vlasnik.Prezime = selitem.Vlasnik.Prezime;
            Avion.BrojSedista = selitem.BrojSedista;
            Avion.KapacitetRezervoara = selitem.KapacitetRezervoara;
            Avion.Nosivost = selitem.Nosivost;
            TipCB.SelectedIndex = selitem.Tip == "Putnicki" ? 0 : 1;
            if (selitem.BrojRaketa == null)
            {
                Avion.BrojRaketa = null;
            }
            else
            {
                Avion.BrojRaketa = selitem.BrojRaketa;
            }
        }
    }
}
