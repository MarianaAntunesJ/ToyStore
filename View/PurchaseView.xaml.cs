using System.Windows;
using System.Windows.Controls;
using ToyStore.ViewModel;
using Xceed.Wpf.Toolkit;

namespace ToyStore.View
{
    public partial class PurchaseView : Page
    {
        private PurchaseViewModel _purchaseViewModel { get; set; }
        public object NavigationCacheMode { get; }
        private CustomersListView _customersListView;
        private ToysListView _toysListView;
        private ValidationView _validationView { get; set; } = new ValidationView();

        public PurchaseView()
        {
            InitializeComponent();
            _purchaseViewModel = new PurchaseViewModel();
            DataContext = _purchaseViewModel;
            CbActive.IsChecked = true;
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            _purchaseViewModel.Query(TxbSearch.Text);
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            if (_purchaseViewModel.Save())
                System.Windows.MessageBox.Show("Purchase was save!", "Save");
            else
                System.Windows.MessageBox.Show("Purchase wasn't save.", "Warning");
        }

    private void DgToys_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DgPurchases.Items.IndexOf(DgPurchases.CurrentItem) >= 0)
        {
            _purchaseViewModel.Select(DgPurchases.Items.IndexOf(DgPurchases.CurrentItem));
        }
    }

    private void BtNewUser_Click(object sender, RoutedEventArgs e)
    {
        _purchaseViewModel.ClearView();
        CbActive.IsChecked = true;
    }

    private void MtxbPhone_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var nome = (MaskedTextBox)sender;
        if (nome.MaskedTextProvider.AssignedEditPositionCount == 0)
            nome.CaretIndex = 0;
    }

        private void BtnCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (_customersListView == null)
                _customersListView = new CustomersListView(_purchaseViewModel, this);
            NavigationService.Navigate(_customersListView);
        }

        private void BtnToys_Click(object sender, RoutedEventArgs e)
        {
            if (_toysListView == null)
                _toysListView = new ToysListView(_purchaseViewModel, this);
            NavigationService.Navigate(_toysListView);
        }
    }
}

