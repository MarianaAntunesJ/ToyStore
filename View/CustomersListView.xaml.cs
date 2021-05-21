using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ToyStore.ViewModel;

namespace ToyStore.View
{
    public partial class CustomersListView : Page
    {
        private CustomerOfPurchaseViewModel _customerOfPurchaseViewModel { get; set; }
        private PurchaseView _purchaseView;

        public CustomersListView(PurchaseViewModel purchaseViewModel, PurchaseView purchaseView)
        {
            InitializeComponent();
            _customerOfPurchaseViewModel = new CustomerOfPurchaseViewModel(purchaseViewModel);
            DataContext = _customerOfPurchaseViewModel;
            _purchaseView = purchaseView;
        }

        private void BtnCancelar(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(_purchaseView);
        }

        private void BtnSalvar(object sender, RoutedEventArgs e)
        {
            _customerOfPurchaseViewModel.InsertCustomer();
            NavigationService.Navigate(_purchaseView);
        }

        private void DgCustomersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgCustomersList.Items.IndexOf(DgCustomersList.CurrentItem) >= 0)
            {
                _customerOfPurchaseViewModel.Select(DgCustomersList.Items.IndexOf(DgCustomersList.CurrentItem));
            }
        }

        private void TxBPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            _customerOfPurchaseViewModel.Query(TxBSearch.Text);
        }
    }
}
