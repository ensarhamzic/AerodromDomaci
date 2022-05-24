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
            Avioni = new ObservableCollection<Avion>();
            UcitajAvione();
            InitializeComponent();
        }

        private void UcitajAvione()
        {
            DataTable dt = baza.Read() as DataTable;
            Avioni.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Avion temp = new Avion
                {
                    Id = Convert.ToInt32(dt.Rows[i]["id"]),
                    Tip = (string)dt.Rows[i]["ime_tipa"],
                    SerijskiBroj = (string)dt.Rows[i]["ser_br"],
                    RegistracioniBroj = (string)dt.Rows[i]["reg_br"],
                    Vlasnik = new Vlasnik(dt.Rows[i]["ime"].ToString(), dt.Rows[i]["prezime"].ToString()),
                    BrojSedista = Convert.ToInt32(dt.Rows[i]["br_sedista"]),
                    KapacitetRezervoara = Convert.ToInt32(dt.Rows[i]["kapacitet_rez"]),
                    Nosivost = Convert.ToInt32(dt.Rows[i]["nosivost"]),

                };
                if (dt.Rows[i]["broj_raketa"] is DBNull)
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
            if (Validacija() == false) return;
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
                    Nosivost = Avion.Nosivost,
                    BrojRaketa = Avion.BrojRaketa
                });
            }
            else
            {
                MessageBox.Show("Doslo je do greske!");
            }
            Avion.SerijskiBroj = null;
            Avion.RegistracioniBroj = null;
            Avion.Vlasnik = new Vlasnik();
            Avion.BrojSedista = 0;
            Avion.KapacitetRezervoara = 0;
            Avion.Nosivost = 0;
            Avion.BrojRaketa = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            ComboBoxItem cbi = (ComboBoxItem)combo.SelectedItem;
            if (cbi.Content.ToString() == "Putnicki")
            {
                RaketeTB.IsEnabled = false;
            }
            else
            {
                RaketeTB.IsEnabled = true;
            }
            Avion.BrojRaketa = null;
            Avion.Tip = cbi.Content.ToString();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var selitem = dataGrid.SelectedItem as Avion;
            if (selitem is null)
                return;
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

        private void AzurirajAvion(object sender, RoutedEventArgs e)
        {
            if (Validacija() == false) return;
            baza.Update(Avion);
            UcitajAvione();
        }

        private void ObrisiAvion(object sender, RoutedEventArgs e)
        {
            baza.Delete(Avion.Id);
            UcitajAvione();
        }

        private bool Validacija()
        {
            if (TipCB.SelectedIndex == -1 || SerTB.Text == "" || RegTB.Text == "" || SerTB.Text.Length > 30 || RegTB.Text.Length > 30
                || ImeTB.Text == "" || PrezimeTB.Text == "" || ImeTB.Text.Length > 20 || PrezimeTB.Text.Length > 20
                || int.TryParse(SedTB.Text, out _) == false
                || int.TryParse(KapTB.Text, out _) == false || int.TryParse(NosivostTB.Text, out _) == false
                || (TipCB.SelectedIndex == 1 && int.TryParse(RaketeTB.Text, out _) == false)
                || Avion.BrojSedista < 0 || Avion.KapacitetRezervoara < 0 || Avion.Nosivost < 0)
            {
                MessageBox.Show("Morate popuniti sva polja ispravno.\n" +
                    "Broj sedista, kapacitet rezervoara i nosivost moraju biti pozitivni brojevi.\n" +
                    "Serijski Broj i Registracioni broj ne smeju biti prazni i moraju imati maximalno 30 karaktera.\n"
                    + "Ime i prezime ne smeju biti prazni i moraju imati maximalno 20 karaktera.\n");
                return false;
            }
            return true;
        }
    }
}
