using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using modele;
using NivelStocareDate;
namespace Interface
{
    public partial class MainWindow : Window
    {
        private AdministareContacteFisier _storage;
        public MainWindow()
        {
            InitializeComponent();

            _storage = new AdministareContacteFisier(@"C:\Users\\mari_\\source\\repos\\sabinaiurcu\\Agenda-telefonica\\Contacte.txt");

            
            cmbCategorie.ItemsSource = Enum.GetValues(typeof(CategorieContact));


            RefreshLista();
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {

            string nume = txtNume.Text.Trim();
            string prenume = txtPrenume.Text.Trim();
            string telefon = txtTelefon.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(nume) || string.IsNullOrEmpty(prenume)
                || string.IsNullOrEmpty(telefon) || string.IsNullOrEmpty(email))
            {
                lblEroare.Content = "Completați toate câmpurile!";
                return;
            }

            CategorieContact categorie = (CategorieContact)cmbCategorie.SelectedItem;

          
            MetodaContact metode = 0;
            if (chkTelefon.IsChecked == true) metode |= MetodaContact.Telefon;
            if (chkEmail.IsChecked == true) metode |= MetodaContact.Email;
            if (chkWhatsApp.IsChecked == true) metode |= MetodaContact.WhatsApp;

            if (metode == 0)
            {
                lblEroare.Content = "Selectați cel puțin o metodă!";
                return;
            }

            Contact contact = new Contact(0, nume, prenume, telefon, email,
                                          categorie, metode);

            if (!contact.SetTelefon(telefon))
            {
                lblEroare.Content = "Telefonul trebuie să aibă exact 10 cifre!";
                return;
            }

            _storage.AddContact(contact);
            RefreshLista();
            ClearForm();
        }

        private void RefreshLista()
        {
            listViewContacte.ItemsSource = null;
            listViewContacte.ItemsSource = _storage.Getcontacte();
        }

        private void ClearForm()
        {
            txtNume.Text = "";
            txtPrenume.Text = "";
            txtTelefon.Text = "";
            txtEmail.Text = "";
            cmbCategorie.SelectedIndex = 0;
            chkTelefon.IsChecked = true;
            chkEmail.IsChecked = false;
            chkWhatsApp.IsChecked = false;
        }
    }

}
    
