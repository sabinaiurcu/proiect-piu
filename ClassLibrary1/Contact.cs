using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace modele

{
    public enum CategorieContact
    {
        Familie,
        Serviciu,
        Prieten,
        Altele
    }

    public enum MetodaContact
    {
        Telefon = 1,
        Email = 2,
        WhatsApp = 4,
   
    }

    public class Contact : INotifyPropertyChanged
    {
        private const char SEPARATOR = ' ';
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';
        private const char SEPARATOR_SECUNDAR_FISIER = ' ';
        private const bool SUCCES = true;
        public const int LUNGIME_MINIMA_TELEFON = 10;
        public const int LUNGIME_MAXIMA_TELEFON = 10;

        private const int ID = 0;
        private const int NUME = 1;
        private const int PRENUME = 2;
        private const int TELEFON = 3;
        private const int EMAIL = 4;
        private const int CATEGORIE = 5;
        private const int METODE = 6;

        private string _nume;
        private string _prenume;

        public int Id { get; set; }
        public string Nume
        {
            get => _nume;
            set { _nume = value; OnPropertyChanged(); }
        }
        public string Prenume
        {
            get => _prenume;
            set { _prenume = value; OnPropertyChanged(); }
        }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public CategorieContact Categorie { get; set; }
        public MetodaContact Metode { get; set; }
        public bool AreTelefon { get; set; }
        public bool AreEmail { get; set; }
        public bool AreWhatsApp { get; set; }

        public Contact(int id, string nume, string prenume, string telefon, string email,
                CategorieContact categorie, bool areTelefon, bool areEmail, bool areWhatsApp)
        {
            Id = id;
            Nume = nume;
            Prenume = prenume;
            Telefon = telefon;
            Email = email;
            Categorie = categorie;
            AreTelefon = areTelefon;
            AreEmail = areEmail;
            AreWhatsApp = areWhatsApp;
        }
        public Contact() { }
        public Contact(string linieFisier)
        {
            string[] dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);
            this.Id = Convert.ToInt32(dateFisier[ID]);
            this.Nume = dateFisier[NUME];
            this.Prenume = dateFisier[PRENUME];
            this.Telefon = dateFisier[TELEFON];
            this.Email = dateFisier[EMAIL];
            this.Categorie = (CategorieContact)Enum.Parse(
                                typeof(CategorieContact), dateFisier[5]);
            bool.TryParse(dateFisier[6], out bool areTelefon);
            bool.TryParse(dateFisier[7], out bool areEmail);
            bool.TryParse(dateFisier[8], out bool areWhatsapp);

        }
        public string ConversieLaSirPentruFisier()
        {
            return string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}",
                SEPARATOR_PRINCIPAL_FISIER,
                Id,
                Nume ?? "NECUNOSCUT",
                Prenume ?? "NECUNOSCUT",
                Telefon,
                Email,
                Categorie.ToString(),
                AreTelefon.ToString(),
                AreEmail.ToString(),
                AreWhatsApp.ToString());
        }
        private bool ValideazaTelefon(string telefon)
        {
            if (telefon.Length != LUNGIME_MINIMA_TELEFON)
                return false;

            foreach (char c in telefon)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }
        public bool SetTelefon(string telefon)
        {
            if (ValideazaTelefon(telefon) == SUCCES)
            {
                Telefon = telefon;
                return true;
            }

            Console.WriteLine("Telefon invalid! Trebuie sa aiba exact 10 cifre.");
            return false;
        }



        public string info()
        {

            string info = $"ID:{Id} Nume:{Nume} Prenume:{Prenume} Telefon:{Telefon} Email:{Email}";
            return info;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
        

    }
      
}