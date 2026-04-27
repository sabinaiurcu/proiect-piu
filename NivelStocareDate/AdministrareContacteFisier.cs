using modele;

namespace NivelStocareDate

{
    public class AdministareContacteFisier : IStocareData
    {
        private const int ID_PRIMUL_CONTACT = 1;
        private const int INCREMENT = 1;
        private string numeFisier;


        public AdministareContacteFisier(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }
        public bool UpdateContact(Contact contactActualizat)
        {
            List<Contact> contacte = Getcontacte();
            bool gasit = false;

            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (Contact c in contacte)
                {
                    if (c.Id == contactActualizat.Id)
                    {
                        sw.WriteLine(contactActualizat.ConversieLaSirPentruFisier());
                        gasit = true;
                    }
                    else
                    {
                        sw.WriteLine(c.ConversieLaSirPentruFisier());
                    }
                }
            }

            return gasit;
        }
        public bool DeleteContact(int id)
        {
            List<Contact> contacte = Getcontacte();
            bool gasit = false;

            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (Contact c in contacte)
                {
                    if (c.Id == id)
                        gasit = true;      
                    else
                        sw.WriteLine(c.ConversieLaSirPentruFisier());
                }
            }

            return gasit;
        }

        public void AddContact(Contact contact)

        {
            contact.Id = GetNextId();
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(contact.ConversieLaSirPentruFisier());
            }

        }
        public List<Contact> Getcontacte()
        {
            List<Contact> contacte = new List<Contact>();

            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    contacte.Add(new Contact(linieFisier));
                }
            }

            return contacte;
        }

        public Contact GetContact(string nume, string prenume)
        {
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;


                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Contact contact = new Contact(linieFisier);
                    if (contact.Nume.Equals(nume) && contact.Prenume.Equals(prenume))
                        return contact;
                }
            }

            return null;
        }

        public Contact GetContact(int id)
        {
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;


                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    Contact contact = new Contact(linieFisier);
                    if (contact.Id == id)
                        return contact;
                }
            }

            return null;
        }

        private int GetNextId()
        {
            int Id = ID_PRIMUL_CONTACT;

            List<Contact> contacte = Getcontacte();

            if (contacte.Count == 0)
            {
                return 1;
            }

            return contacte.Last().Id + INCREMENT;

        }






    }
}