using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda_Persoane
{
    public enum Categorie : int
    {
        Prieteni,
        Colegi,
        Rude,
        Diversi
    };

    public class Persoana
    {
        private string nume;
        private DateTime dataNasterii;
        private string telefon;
        private string adresa;
        private Categorie categorie;  
        

        public Persoana(string nume,DateTime dataNasterii, string telefon,string adresa ,Categorie categorie)
        {
            this.nume = nume;
            this.dataNasterii = dataNasterii;
            this.telefon = telefon;
            this.adresa = adresa;
            this.categorie = categorie;
        }

        [Description("Nume complet al persoanei"), Category("Date persoane")]
        public string Nume
        {
            get { return nume; }
            set { nume = value; }
        }
        [Description("Data nasterii a persoanei"), Category("Date persoane")]
        public DateTime DataNasterii
        {
            get { return dataNasterii; }
            set { dataNasterii = value; }
        }
        [Description("Numar de telefon al persoanei"), Category("Date persoane")]
        public string Telefon
        {
            get { return telefon; }
            set { telefon = value; }
        }
        [Description("Adresa persoanei"), Category("Date persoane")]
        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }

        [Description("Categorie persoana"), Category("Date persoane")]
        public Categorie Categorie
        {
            get { return categorie; }
            set { categorie = value; }
        }
    }
}