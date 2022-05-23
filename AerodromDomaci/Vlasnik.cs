

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AerodromDomaci
{
    public class Vlasnik : INotifyPropertyChanged
    {
        private string ime;
        private string prezime;

        public Vlasnik()
        {
        }

        public Vlasnik(string ime, string prezime)
        {
            Ime = ime;
            Prezime = prezime;
        }

        public string Ime
        {
            get { return ime; }
            set
            {
                ime = value;
                OnPropertyChanged();
            }
        }

        public string Prezime
        {
            get { return prezime; }
            set
            {
                prezime = value;
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