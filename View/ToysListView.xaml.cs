using System.Windows.Controls;
using ToyStore.ViewModel;

namespace ToyStore.View
{
    public partial class ToysListView : Page
    {
        private ToysOfPurchaseViewModel _toysOfPurchaseViewModel { get; set; }
        private PurchaseView _purchaseView;

        public ToysListView(PurchaseViewModel purchaseViewModel, PurchaseView purchaseView)
        {
            InitializeComponent();
            _toysOfPurchaseViewModel = new ToysOfPurchaseViewModel(purchaseViewModel);
            DataContext = _toysOfPurchaseViewModel;
            _purchaseView = purchaseView;
        }

        private void DgAllToys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DgSelectedToys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnSalvar(object sender, System.Windows.RoutedEventArgs e)
        {
            _toysOfPurchaseViewModel.InsertToys();
            NavigationService.Navigate(_purchaseView);
        }

        private void BtnCancelar(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(_purchaseView);
        }
    }
}
