using modele;
using NivelStocareDate;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Interface
{
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        private AdministareContacteFisier _storage;
        private AdministrareMesajeFisier _storageMesaje;
        private Contact _contactSelectat = null;
        private int _idMesajEditat = 0;

        private Contact _contactCurent;
        public Contact ContactCurent
        {
            get => _contactCurent;
            set { _contactCurent = value; OnPropertyChanged(); }
        }
        public MainWindow()
        {
            InitializeComponent();
            cmbCategorie.ItemsSource = new List<string> { "Familie", "Serviciu", "Prieten", "Altele" };
            lstMetode.ItemsSource = new List<string> { "Telefon", "Email", "WhatsApp" };
            cmbTipMesaj.ItemsSource = Enum.GetValues(typeof(TipMesaj));
            cmbStatusMesaj.ItemsSource = Enum.GetValues(typeof(StatusMesaj));
            cmbTipMesaj.SelectedIndex = 0;
            cmbStatusMesaj.SelectedIndex = 0;

            ContactCurent = new Contact(); 

            _storage = new AdministareContacteFisier(@"C:\Users\\mari_\\source\\repos\\sabinaiurcu\\Agenda-telefonica\\Contacte.txt");
            _storageMesaje = new AdministrareMesajeFisier( @"C:\Users\\mari_\\source\\repos\\sabinaiurcu\\Agenda-telefonica\\Mesaje.txt");
            RefreshLista();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            if (dtpDataNasterii.SelectedDate == null)
            {
                lblEroare.Content = "Selectați data nașterii!";
                return;
            }
            DateTime dataNasterii = dtpDataNasterii.SelectedDate ?? DateTime.Today;

            CategorieContact categorie = GetCategorieSelectata();

            bool areaTelefon = lstMetode.SelectedItems.Contains("Telefon");
            bool areEmail = lstMetode.SelectedItems.Contains("Email");
            bool areWhatsApp = lstMetode.SelectedItems.Contains("WhatsApp");

            if (!areaTelefon && !areEmail && !areWhatsApp)
            {
                lblEroare.Content = "Selectați cel puțin o metodă!";
                return;
            }

            Contact contact = new Contact(0, nume, prenume, telefon, email,
                                          categorie, areaTelefon, areEmail, areWhatsApp);
          
            if (!contact.SetTelefon(telefon))
            {
                lblEroare.Content = "Telefonul trebuie să aibă exact 10 cifre!";
                return;
            }

            _storage.AddContact(contact);
            RefreshLista();
            ClearForm();
        }
        private void listViewContacte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _contactSelectat = listViewContacte.SelectedItem as Contact;

            if (_contactSelectat != null)
                btnVeziMesaje.Visibility = Visibility.Visible;
            else
                btnVeziMesaje.Visibility = Visibility.Collapsed;
        }
        private void btnVeziMesaje_Click(object sender, RoutedEventArgs e)
        {
            if (_contactSelectat == null) return;

           
            btnMenuMesaje_Click(null, null);

            
            lblContactSelectat.Content = $"Contact: {_contactSelectat.Nume} {_contactSelectat.Prenume}";

            
            RefreshListaMesaje();
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
            
        }

        private CategorieContact GetCategorieSelectata()
        {
            return (CategorieContact)cmbCategorie.SelectedIndex;
        }



        private void btnMenuAdministrare_Click(object sender, RoutedEventArgs e)
        {
            panelAdministrare.Visibility = Visibility.Visible;
            panelCauta.Visibility = Visibility.Collapsed;
            panelMesaje.Visibility = Visibility.Collapsed;

            btnMenuAdministrare.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2980B9"));
            btnMenuCauta.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2C3E50"));
            btnMenuMesaje.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2C3E50"));
        }

        private void btnMenuCauta_Click(object sender, RoutedEventArgs e)
        {
            panelAdministrare.Visibility = Visibility.Collapsed;
            panelCauta.Visibility = Visibility.Visible;
            panelMesaje.Visibility = Visibility.Collapsed;
            btnMenuCauta.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2980B9"));
            btnMenuAdministrare.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2C3E50"));
            btnMenuMesaje.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2C3E50"));
        }
        private void btnMenuMesaje_Click(object sender, RoutedEventArgs e)
        {
            panelAdministrare.Visibility = Visibility.Collapsed;
            panelCauta.Visibility = Visibility.Collapsed;
            panelMesaje.Visibility = Visibility.Visible;

            btnMenuMesaje.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2980B9"));
            btnMenuAdministrare.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2C3E50"));
            btnMenuCauta.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2C3E50"));
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

            List<Contact> rezultate = _storage.CautaContacteDupaNume(numeCautat);

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
    
        private void btnAdaugaMesaj_Click(object sender, RoutedEventArgs e)
        {
            if (_contactSelectat == null)
            {
                lblEroareMesaj.Content = "Selectati mai intai un contact din Administrare!";
                return;
            }

            string continut = txtContinutMesaj.Text.Trim();
            if (string.IsNullOrEmpty(continut))
            {
                lblEroareMesaj.Content = "Introduceti continutul mesajului!";
                return;
            }

            TipMesaj tip = (TipMesaj)cmbTipMesaj.SelectedItem;
            StatusMesaj status = (StatusMesaj)cmbStatusMesaj.SelectedItem;

            Mesaj mesajNou = new Mesaj(0, continut, _contactSelectat.Id, tip, status);
            _storageMesaje.AddMesaj(mesajNou);

            lblEroareMesaj.Content = "";
            RefreshListaMesaje();
            ClearFormMesaj();
        }

        
        private void btnEditeazaMesaj_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            Mesaj m = _storageMesaje.GetMesaj(id);
            if (m == null) return;

            _idMesajEditat = id;
            txtContinutMesaj.Text = m.Continut;
            cmbTipMesaj.SelectedItem = m.Tip;
            cmbStatusMesaj.SelectedItem = m.Status;

            SetModEditareMesaj(true);
        }

        
        private void btnSalveazaMesaj_Click(object sender, RoutedEventArgs e)
        {
            string continut = txtContinutMesaj.Text.Trim();
            if (string.IsNullOrEmpty(continut))
            {
                lblEroareMesaj.Content = "Introduceti continutul!";
                return;
            }

            TipMesaj tip = (TipMesaj)cmbTipMesaj.SelectedItem;
            StatusMesaj status = (StatusMesaj)cmbStatusMesaj.SelectedItem;

            Mesaj actualizat = new Mesaj(_idMesajEditat, continut, _contactSelectat.Id, tip, status);
            _storageMesaje.UpdateMesaj(actualizat);

            lblEroareMesaj.Content = "";
            RefreshListaMesaje();
            ClearFormMesaj();
            SetModEditareMesaj(false);
        }

       
        private void btnStergeMesaj_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;

            MessageBoxResult confirmare = MessageBox.Show(
                "Esti sigur ca vrei sa stergi acest mesaj?",
                "Confirmare",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirmare == MessageBoxResult.Yes)
            {
                _storageMesaje.DeleteMesaj(id);
                RefreshListaMesaje();
            }
        }

        private void btnAnuleazaMesaj_Click(object sender, RoutedEventArgs e)
        {
            ClearFormMesaj();
            SetModEditareMesaj(false);
        }

        private void RefreshListaMesaje()
        {
            if (_contactSelectat != null)
                listViewMesaje.ItemsSource = _storageMesaje.GetMesajeDupaContact(_contactSelectat.Id);
            else
                listViewMesaje.ItemsSource = null;
        }

        private void ClearFormMesaj()
        {
            txtContinutMesaj.Text = "";
            cmbTipMesaj.SelectedIndex = 0;
            cmbStatusMesaj.SelectedIndex = 0;
            lblEroareMesaj.Content = "";
            _idMesajEditat = 0;
        }

        private void SetModEditareMesaj(bool editare)
        {
            if (editare)
            {
                lblTitluMesaj.Content = "Editare mesaj";
                btnAdaugaMesaj.Visibility = Visibility.Collapsed;
                btnSalveazaMesaj.Visibility = Visibility.Visible;
                btnAnuleazaMesaj.Visibility = Visibility.Visible;
            }
            else
            {
                lblTitluMesaj.Content = "Mesaj nou";
                btnAdaugaMesaj.Visibility = Visibility.Visible;
                btnSalveazaMesaj.Visibility = Visibility.Collapsed;
                btnAnuleazaMesaj.Visibility = Visibility.Collapsed;
            }
        }



    }
}