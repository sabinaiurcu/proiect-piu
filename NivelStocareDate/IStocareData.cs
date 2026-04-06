using modele;


namespace NivelStocareDate
{
  public interface IStocareData
    {
        void AddContact(Contact c);
        List<Contact> Getcontacte();
        Contact GetContact(int id);
        Contact GetContact(string name, string prenume);
        bool UpdateContact(Contact c);
        
    }
}
