using modele;
using NivelStocareDate;
using System.Windows;
using System.Windows.Media;

namespace Interface
{
    public partial class MainWindow : Window
    {
        private AdministareContacteFisier _storage;

        public MainWindow()
        {
            InitializeComponent();

            _storage = new AdministareContacteFisier(@"C:\Users\\mari_\\source\\repos\\sabinaiurcu\\Agenda-telefonica\\Contacte.txt");

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

            CategorieContact categorie = GetCategorieSelectata();

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
            rbFamilie.IsChecked = true;
            chkTelefon.IsChecked = true;
            chkEmail.IsChecked = false;
            chkWhatsApp.IsChecked = false;
        }

        private CategorieContact GetCategorieSelectata()
        {
            if (rbServciu.IsChecked == true) return CategorieContact.Serviciu;
            if (rbPrieten.IsChecked == true) return CategorieContact.Prieten;
            if (rbAltele.IsChecked == true) return CategorieContact.Altele;
            return CategorieContact.Familie;
        }

        

        private void btnMenuAdministrare_Click(object sender, RoutedEventArgs e)
        {
            panelAdministrare.Visibility = Visibility.Visible;
            panelCauta.Visibility = Visibility.Collapsed;

            btnMenuAdministrare.Background = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString("#2980B9"));
            btnMenuCauta.Background = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString("#2C3E50"));
        }

        private void btnMenuCauta_Click(object sender, RoutedEventArgs e)
        {
            panelAdministrare.Visibility = Visibility.Collapsed;
            panelCauta.Visibility = Visibility.Visible;

            btnMenuCauta.Background = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString("#2980B9"));
            btnMenuAdministrare.Background = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString("#2C3E50"));

        }
        private void btnCauta_Click(object sender, RoutedEventArgs e)
        {
            string numeCautat = txtCauta.Text.Trim();

            if (string.IsNullOrEmpty(numeCautat))
            {
                lblCautaMsg.Content = "Introduceți un nume pentru căutare!";
                lblCautaMsg.Foreground = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString("#E74C3C"));
                dgRezultate.ItemsSource = null;
                return;
            }

            List<Contact> rezultate = _storage.Getcontacte()
                .Where(c => c.Nume.ToLower().Contains(numeCautat.ToLower()))
                .ToList();

            if (rezultate.Count == 0)
            {
                lblCautaMsg.Content = "Nu a fost găsit niciun contact cu acest nume.";
                lblCautaMsg.Foreground = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString("#E74C3C"));
                dgRezultate.ItemsSource = null;
            }
            else
            {
                lblCautaMsg.Content = $"Au fost găsite {rezultate.Count} contacte.";
                lblCautaMsg.Foreground = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString("#27AE60"));
                dgRezultate.ItemsSource = rezultate;
            }
        }
       
    }
}