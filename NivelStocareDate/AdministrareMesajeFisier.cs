using modele;

namespace NivelStocareDate
{
    public class AdministrareMesajeFisier:IStocareMesaje
    {
        private const int ID_PRIMUL_MESAJ = 1;
        private const int INCREMENT = 1;
        private string numeFisier;

        public AdministrareMesajeFisier(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream stream = File.Open(numeFisier, FileMode.OpenOrCreate);
            stream.Close();
        }

        public void AddMesaj(Mesaj mesaj)
        {
            mesaj.Id = GetNextId();
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(mesaj.ConversieLaSirPentruFisier());
            }
        }

        public List<Mesaj> GetMesaje()
        {
            List<Mesaj> mesaje = new List<Mesaj>();
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    mesaje.Add(new Mesaj(linie));
                }
            }
            return mesaje;
        }

        public Mesaj GetMesaj(int id)
        {
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    Mesaj m = new Mesaj(linie);
                    if (m.Id == id)
                        return m;
                }
            }
            return null;
        }

        public List<Mesaj> GetMesajeDupaContact(int contactId)
        {
            return GetMesaje().Where(m => m.ContactId == contactId).ToList();
        }

        public bool UpdateMesaj(Mesaj actualizat)
        {
            List<Mesaj> mesaje = GetMesaje();
            bool gasit = false;
            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (Mesaj m in mesaje)
                {
                    if (m.Id == actualizat.Id)
                    {
                        sw.WriteLine(actualizat.ConversieLaSirPentruFisier());
                        gasit = true;
                    }
                    else
                    {
                        sw.WriteLine(m.ConversieLaSirPentruFisier());
                    }
                }
            }
            return gasit;
        }

        private int GetNextId()
        {
            List<Mesaj> mesaje = GetMesaje();
            if (mesaje.Count == 0)
                return ID_PRIMUL_MESAJ;
            return mesaje.Last().Id + INCREMENT;
        }
    }

}

