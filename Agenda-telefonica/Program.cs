using modele;

using NivelStocareDate;
using System;


namespace Program
{
    class Program
    {
        public static void Main()
        {
             Agenda agenda = new Agenda();
             int opt;
   
            IStocareData admincontacte = StocareFactory.GetAdministratorStocare();
            IStocareMesaje adminMesaje = new AdministrareMesajeMem();


            Contact contactNou = null;
            Mesaj mesajNou = null;

            List<Contact> contacte = new List<Contact>();
            List<Mesaj> mesaje = new List<Mesaj>();
            do
            {

                Console.WriteLine("1. Adaugare contact");
                Console.WriteLine("2. Afiseaza contacte");
                Console.WriteLine("3. Cauta contact");
                Console.WriteLine("4. Modifica contact");
                Console.WriteLine("5. Sterge contact");
                Console.WriteLine("6. Salvare Contact");
                Console.WriteLine("7. Adaugare mesaj");
                Console.WriteLine("8. Afiseaza mesaje");
                Console.WriteLine("9. Cauta mesaje dupa contact");
                Console.WriteLine("0. Iesire");
                Console.WriteLine("Optiunea dvs este:");
                opt = int.Parse(Console.ReadLine());

                switch (opt)
                {
                    case 1:
                        {
                            contactNou = agenda.CitireContactTastatura();
                            break;
                        }
                    case 2:
                        {
                            contacte = admincontacte.Getcontacte();
                            agenda.afisareContacte(contacte);
                            break;
                        }
                    case 3:
                        {
                            agenda.cauta(admincontacte.Getcontacte());
                            break;
                        }
                    case 4:
                        {
                            agenda.modifica(admincontacte.Getcontacte());
                            break;
                        }
                    case 5:
                        {
                            agenda.sterge(admincontacte.Getcontacte());
                            break;
                        }
                    case 6:
                        {
                            if (contactNou != null)
                            {
                                admincontacte.AddContact(contactNou);
                                Console.WriteLine("Contact salvat!");
                            }
                            else
                            {
                                Console.WriteLine("Contactul nu a fost initializat");
                            }
                            break;
                        }
                    case 7:
                        {
                            mesajNou = agenda.CitireMesajTastatura();
                            if (mesajNou != null)
                            {
                                adminMesaje.AddMesaj(mesajNou);
                                Console.WriteLine("Mesaj salvat!");
                            }
                            break;
                        }
                    case 8:
                        {
                            mesaje = adminMesaje.GetMesaje();
                            agenda.AfisareMesaje(mesaje);
                            break;
                        }
                    case 9:
                        {
                            Console.WriteLine("Introduceti id-ul contactului:");
                            int contactId = int.Parse(Console.ReadLine());
                            mesaje = adminMesaje.GetMesajeDupaContact(contactId);
                            agenda.AfisareMesaje(mesaje);
                            break;
                        }
                }

            } while (opt != 0);

        }
    }
}
