using modele;

namespace NivelStocareDate
{
    public class AdministrareContacteMem: IStocareData
    {
        private List<Contact> contacte;

        public AdministrareContacteMem()
        { 
        contacte=new List<Contact>();
        }

        public void AddContact(Contact contact)
        {
            contact.Id = GetNextId();
            contacte.Add(contact);
            
        }
        public List<Contact> Getcontacte()
        {
            return contacte;
        }

        public Contact? GetContact(int id)
        {foreach (Contact contact in contacte)
            {
                if (contact.Id == id)
                {
                    return contact;
                }
            }
        return null;
        }

        public Contact? GetContact(string nume, string prenume)
        {
            return contacte?.FirstOrDefault(contact => contact.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase) && contact.Prenume.Equals(prenume, StringComparison.OrdinalIgnoreCase));
        }
        public bool UpdateContact(Contact contactActualizat)
        {
            foreach (Contact c in contacte)
            {
                if (c.Id == contactActualizat.Id)
                {
                    c.Nume = contactActualizat.Nume;
                    c.Prenume = contactActualizat.Prenume;
                    c.Telefon = contactActualizat.Telefon;
                    c.Email = contactActualizat.Email;
                    c.Categorie = contactActualizat.Categorie;
                    c.Metode = contactActualizat.Metode;
                    return true;
                }
            }
            return false;
        }
        public int GetNextId()
        { if(contacte.Count == 0)
            {
                return 1;
            }
            return contacte.Last().Id + 1;
        
        }

    }
}
