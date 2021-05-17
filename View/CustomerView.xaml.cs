using System;
using System.Windows;
using System.Windows.Controls;
using ToyStore.ViewModel;
using Xceed.Wpf.Toolkit;

namespace ToyStore.View
{
    public partial class CustomerView : Page
    {
        private CustomerViewModel _customerViewModel { get; set; }
        private ValidationView _validationView { get; set; } = new ValidationView();

        public CustomerView()
        {
            InitializeComponent();
            _customerViewModel = new CustomerViewModel();
            DataContext = _customerViewModel;
            CbActive.IsChecked = true;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
            else if (e.PropertyName == "CPF")
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            _customerViewModel.Query(TxbSearch.Text);
        }

        private void FieldValidationViewWrong()
        {
            bool NameInvalid = !(_validationView.ValidateName(TxBName.Text));
            bool PhoneInvalid = !(_validationView.ValidatePhone(MtxbPhone.Text));
            bool CpfInvalid = !(_validationView.ValidateCPF(MtxbCPF.Text));


            if (NameInvalid || PhoneInvalid || CpfInvalid)
            {

                if (NameInvalid && PhoneInvalid && CpfInvalid)
                    System.Windows.MessageBox.Show("Fields Name, Phone and CPF are wrong filled. Please, fill them with correct information.", "Warning");

                else if (NameInvalid && PhoneInvalid)
                    System.Windows.MessageBox.Show("Fields Name and Phone are wrong filled. Please, fill them with correct information.", "Warning");

                else if (NameInvalid && CpfInvalid)
                    System.Windows.MessageBox.Show("Fields Name and CPF are wrong filled. Please, fill them with correct information.", "Warning");

                else if (PhoneInvalid && CpfInvalid)
                    System.Windows.MessageBox.Show("Fields Phone and CPF are wrong filled. Please, fill them with correct information.", "Warning");

                else if (NameInvalid)
                    System.Windows.MessageBox.Show("Field Name is wrong filled. Please, fill them with correct information.", "Warning");

                else if (PhoneInvalid)
                    System.Windows.MessageBox.Show("Field Phone is wrong filled. Please, fill them with correct information.", "Warning");

                else if (CpfInvalid)
                    System.Windows.MessageBox.Show("Field CPF is wrong filled. Please, fill them with correct information.", "Warning");
            }
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            if (_validationView.ValidatePerson(TxBName.Text, MtxbPhone.Text, MtxbCPF.Text))
            {
                if (_customerViewModel.Save(MtxbCPF.Text))
                {
                    RbFeminine.IsChecked = false;
                    RbMasculine.IsChecked = false;
                    RbOther.IsChecked = false;
                    MtxbCPF.Text = string.Empty;
                    System.Windows.MessageBox.Show("Cliente salvo!", "Salvo");
                }
                else
                    FieldValidationViewWrong();
            }
            else
                FieldValidationViewWrong();
        }

        private void RbGender_Checked(object sender, RoutedEventArgs e)
        {
            _customerViewModel.Customer.Gender = (sender as RadioButton).Content.ToString();
        }

        private void DgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgCustomers.Items.IndexOf(DgCustomers.CurrentItem) >= 0)
            {
                _customerViewModel.Select(DgCustomers.Items.IndexOf(DgCustomers.CurrentItem));

                MtxbCPF.Text = _customerViewModel.Customer.CPF;

                if (_customerViewModel.Customer.Gender.Equals("FEMININE"))
                    RbFeminine.IsChecked = true;
                else if (_customerViewModel.Customer.Gender.Equals("MASCULINE"))
                    RbMasculine.IsChecked = true;
                else if (_customerViewModel.Customer.Gender.Equals("OTHER"))
                    RbOther.IsChecked = true;
            }
        }

        private void BtNewUser_Click(object sender, RoutedEventArgs e)
        {
            _customerViewModel.ClearView();
            MtxbCPF.Text = string.Empty;
            CbActive.IsChecked = true;
        }

        private void MtxbPhone_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var nome = (MaskedTextBox)sender;
            if (nome.MaskedTextProvider.AssignedEditPositionCount == 0)
                nome.CaretIndex = 0;
        }
    }
}
