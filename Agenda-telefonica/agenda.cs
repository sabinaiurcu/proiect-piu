using modele;
using System;
using System.Collections.Generic;


namespace Program
{
    class Agenda
    {
        public Contact CitireContactTastatura()
        {
            Console.WriteLine("Introd numele contactului:");
            int urmatorul_id = 1;
            string nume = Console.ReadLine();

            Console.WriteLine("Introd prenumele contactului:");
            string prenume = Console.ReadLine();

            Console.WriteLine("Telefon:");
            string telefon = Console.ReadLine();

            if (telefon.Length != 10)
            {
                Console.WriteLine("numar invalid");
            }

            Console.WriteLine("Email:");
            string email = Console.ReadLine();

            Console.WriteLine("Categorie: 0=Familie, 1=Serviciu, 2=Prieten, 3=Altul ->");
            int optCategorie = int.Parse(Console.ReadLine());
            CategorieContact categorie = (CategorieContact)optCategorie;

            Console.WriteLine("Contact(aduna valorile dorite): 1=Telefon, 2=Email, 4=WhatsApp, 8=Messenger ->");

            int optMetode = int.Parse(Console.ReadLine());
            MetodaContact metode = (MetodaContact)optMetode;

            Contact contactNou = new Contact(urmatorul_id, nume, prenume, telefon, email,categorie,metode);

            urmatorul_id++;
            return contactNou;

        }
        public void afisareContacte(List<Contact> contacte)
        {
            if (contacte.Count == 0)
            {
                Console.WriteLine("Nu exista niciun contact in agenda.");
                return;
            }

            foreach (Contact c in contacte)
            {
                Console.WriteLine(c.info());
            }
        }
        public void cauta(List<Contact> contacte)
        {
            Console.WriteLine("Introd numele cautat: ");
            string cuvant = Console.ReadLine();
            bool gasit = false;
            foreach (Contact c in contacte)
            {
                if (c.Nume.ToLower() == cuvant.ToLower())
                {
                    gasit = true;
                }
            }
            if (!gasit)
            {
                Console.WriteLine("nu exista contactul");

            }
            else
            {
                Console.WriteLine("contactul exista");
            }
        }
        public void modifica(List<Contact> contacte)
        {
            Console.WriteLine("Id ul contactului pe care doriti sa il modificati: ");
            int id = int.Parse(Console.ReadLine());

            bool gasit = false;
            foreach (Contact c in contacte)
            {
                if (c.Id == id)
                {
                    gasit = true;
                    Console.WriteLine("Nume nou: ");
                    string numeNou = Console.ReadLine();
                    if (numeNou != "")
                    {
                        c.Nume = numeNou;
                    }

                    Console.WriteLine("Prenume nou: ");
                    string prenumeNou = Console.ReadLine();
                    if (prenumeNou != "")
                    {
                        c.Prenume = prenumeNou;
                    }
                }
                Console.WriteLine("Contactul a fost modificat!");
                break;
            }
            if (!gasit)
            {
                Console.WriteLine("Nu exista contact cu acest Id");
            }
        }
        public void sterge(List<Contact> contacte)
        {
            Console.WriteLine("Id ul contactului pe care doriti sa il stergeti: ");
            int id = int.Parse(Console.ReadLine());

            bool gasit = false;
            foreach (Contact c in contacte)
            {
                if (c.Id == id)
                {
                    gasit = true;
                    contacte.Remove(c);
                    Console.WriteLine("Contactul a fost sters!");
                    break;
                }
            }
            if (!gasit)
            {
                Console.WriteLine("Nu exista contact cu acest Id");
            }


        }
        public Mesaj CitireMesajTastatura()
        {
            Console.WriteLine("Introduceti id-ul contactului:");
            int contactId = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti continutul mesajului:");
            string continut = Console.ReadLine();

            Console.WriteLine("Alegeti tipul mesajului(1.SMS, 2.Whatsapp, 3.Email, 4.Altele):");
            int tipmsg = int.Parse(Console.ReadLine());
            TipMesaj tip = (TipMesaj)tipmsg;

            Console.WriteLine("Alegeti statusul mesajului(1.Citit, 2.Important, 3.Neimportant):");
            int statusmsg = int.Parse(Console.ReadLine());
            StatusMesaj status = (StatusMesaj)statusmsg;

            return new Mesaj(0, continut, contactId, tip,status);
        }

        public void AfisareMesaje(List<Mesaj> mesaje)
        {
            if (mesaje.Count == 0)
            {
                Console.WriteLine("Nu exista mesaje.");
                return;
            }
            foreach (Mesaj m in mesaje)
            {
                Console.WriteLine(m.Info());
            }
        }

    }
}
