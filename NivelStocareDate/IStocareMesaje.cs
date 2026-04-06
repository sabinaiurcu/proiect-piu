using modele;


namespace NivelStocareDate
{
    public interface IStocareMesaje
    {
        void AddMesaj(Mesaj m);
        List<Mesaj> GetMesaje();
        Mesaj GetMesaj(int id);
        List<Mesaj> GetMesajeDupaContact(int contactId);
        bool UpdateMesaj(Mesaj m);
    }
}
