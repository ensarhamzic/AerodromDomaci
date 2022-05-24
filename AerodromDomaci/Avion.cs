using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AerodromDomaci
{
    public class Avion : INotifyPropertyChanged
    {
        private int id;
        private string tip;
        private string serijskiBroj;
        private string registracioniBroj;
        private Vlasnik vlasnik;
        private int brojSedista;
        private int kapacitetRezervoara;
        private int nosivost;
        private int? brojRaketa;

        public Avion() {
            this.vlasnik = new Vlasnik();
        }

        public Avion(string serijskiBroj, string registracioniBroj, Vlasnik vlasnik,
            int brojSedista, int kapacitetRezervoara, int nosivost, int? brojRaketa = null)
        {
            this.SerijskiBroj = serijskiBroj;
            this.RegistracioniBroj = registracioniBroj;
            this.Vlasnik = vlasnik;
            this.BrojSedista = brojSedista;
            this.KapacitetRezervoara = kapacitetRezervoara;
            this.Nosivost = nosivost;
            this.BrojRaketa = brojRaketa;
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Tip
        {
            get { return tip; }
            set
            {
                tip = value;
                OnPropertyChanged();
            }
        }

        public string SerijskiBroj
        {
            get
            {
                return serijskiBroj;
            }
            set
            {
                serijskiBroj = value;
                OnPropertyChanged();
            }
        }

        public string RegistracioniBroj
        {
            get
            {
                return registracioniBroj;
            }
            set
            {
                registracioniBroj = value;
                OnPropertyChanged();
            }
        }

        public Vlasnik Vlasnik
        {
            get
            {
                return vlasnik;
            }
            set
            {
                vlasnik = value;
                OnPropertyChanged();
            }
        }

        public int BrojSedista
        {
            get
            {
                return brojSedista;
            }
            set
            {
                brojSedista = value;
                OnPropertyChanged();
            }
        }

        public int KapacitetRezervoara
        {
            get
            {
                return kapacitetRezervoara;
            }
            set
            {
                kapacitetRezervoara = value;
                OnPropertyChanged();
            }
        }

        public int Nosivost
        {
            get
            {
                return nosivost;
            }
            set
            {
                nosivost = value;
                OnPropertyChanged();
            }
        }

        public int? BrojRaketa
        {
            get
            {
                return brojRaketa;
            }
            set
            {
                brojRaketa = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
