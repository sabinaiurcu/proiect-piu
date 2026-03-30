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
            Contact contactNou = null;         

            List<Contact> contacte = new List<Contact>();
            do
            {

                Console.WriteLine("1. Adaugare contact");
                Console.WriteLine("2. Afiseaza contacte");
                Console.WriteLine("3. Cauta contact");
                Console.WriteLine("4. Modifica contact");
                Console.WriteLine("5. Sterge contact");
                Console.WriteLine("6. Salvare Contact");
                Console.WriteLine("0. Iesire");
                Console.WriteLine("Optiunea dvs este:");
                opt = int.Parse(Console.ReadLine());

                switch (opt)
                {
                    case 1:
                        contactNou = agenda.CitireContactTastatura();
                        break;
                    case 2:
                        contacte=admincontacte.Getcontacte();
                        agenda.afisareContacte(contacte);
                        break;
                    case 3:
                        agenda.cauta(contacte);
                        break;
                    case 4:
                        agenda.modifica(admincontacte.Getcontacte());
                        break;
                    case 5:
                        agenda.sterge(admincontacte.Getcontacte());
                        break;
                    case 6:
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

            } while (opt != 0);

        }
    }
}
