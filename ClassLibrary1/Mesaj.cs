using System;


namespace modele
{
    public enum TipMesaj
    {
        SMS = 1,
        WhatsApp = 2,
        Email = 3,
        Altele = 4
    }
    public enum StatusMesaj
    {
        Necitit = 0,
        Citit = 1,
        Salvat = 2,
        Important = 4
    }

    public class Mesaj
    {
        private const char SEPARATOR_FISIER = ';';

        public int Id { get; set; }
        public string Continut { get; set; }
        public int ContactId { get; set; }
        public TipMesaj Tip { get; set; }
        public StatusMesaj Status { get; set; }

        public Mesaj(int id, string continut, int contactId, TipMesaj tip, StatusMesaj status)
        {
            Id = id;
            Continut = continut;
            ContactId = contactId;
            Tip = tip;
            Status = status;
        }

        public string Info()
        {
            return $"ID:{Id} ContactId:{ContactId} Tip:{Tip} Status:{Status} Continut:{Continut}";
        }
        public Mesaj(string linieFisier)
        {
            string[] date = linieFisier.Split(';');
            Id = int.Parse(date[0]);
            Continut = date[1];
            ContactId = int.Parse(date[2]);
            Tip = (TipMesaj)Enum.Parse(typeof(TipMesaj), date[3]);
            Status = (StatusMesaj)Enum.Parse(typeof(StatusMesaj), date[4]);
        }

        public string ConversieLaSirPentruFisier()
        {
            return $"{Id};{Continut};{ContactId};{Tip};{Status}";
        }
    }
}
