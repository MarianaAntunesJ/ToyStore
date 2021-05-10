using System;
using System.Windows;
using System.Windows.Controls;
using ToyStore.ViewModel;

namespace ToyStore.View
{
    public partial class CustomerView : Page
    {
        private CustomerViewModel _customerViewModel { get; set; }

        public CustomerView()
        {
            InitializeComponent();
            _customerViewModel = new CustomerViewModel();
            DataContext = _customerViewModel;
        }
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            _customerViewModel.Query(TxbSearch.Text);
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            if (_customerViewModel.Save())
                System.Windows.MessageBox.Show("Cliente salvo!", "Salvo");
            else
                Xceed.Wpf.Toolkit.MessageBox.Show("Cliente não foi salvo.", "Erro");
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

                if (_customerViewModel.Customer.Gender.Equals("Feminine"))
                    RbFeminine.IsChecked = true;
                else if (_customerViewModel.Customer.Gender.Equals("Masculine"))
                    RbMasculine.IsChecked = true;
                else if (_customerViewModel.Customer.Gender.Equals("Other"))
                    RbOther.IsChecked = true;
            }
        }
    }
}
