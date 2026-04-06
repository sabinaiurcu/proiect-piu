using System;
using System.Linq;
using modele;

namespace NivelStocareDate
{
    public class AdministrareMesajeMem:IStocareMesaje
    {
        private List<Mesaj> mesaje;

        public AdministrareMesajeMem()
        {
            mesaje = new List<Mesaj>();
        }

        public void AddMesaj(Mesaj m)
        {
            m.Id = GetNextId();
            mesaje.Add(m);
        }

        public List<Mesaj> GetMesaje()
        {
            return mesaje;
        }

        public Mesaj GetMesaj(int id)
        {
            return mesaje.FirstOrDefault(m => m.Id == id);
        }

        public List<Mesaj> GetMesajeDupaContact(int contactId)
        {
            return mesaje.Where(m => m.ContactId == contactId).ToList();
        }

        public bool UpdateMesaj(Mesaj actualizat)
        {
            foreach (Mesaj m in mesaje)
            {
                if (m.Id == actualizat.Id)
                {
                    m.Continut = actualizat.Continut;
                    m.ContactId = actualizat.ContactId;
                    m.Tip = actualizat.Tip;
                    m.Status = actualizat.Status;
                    return true;
                }
            }
            return false;
        }

        private int GetNextId()
        {
            if (mesaje.Count == 0)
            {
                return 1;
            }
            return mesaje.Last().Id + 1;
        }
    }
}