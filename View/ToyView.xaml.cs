using System.Windows;
using System.Windows.Controls;
using ToyStore.ViewModel;
using Xceed.Wpf.Toolkit;

namespace ToyStore.View
{
    /// <summary>
    /// Interação lógica para ToyView.xam
    /// </summary>
    public partial class ToyView : Page
    {
        private ToyViewModel _toyViewModel { get; set; }
        private ValidationView _validationView { get; set; } = new ValidationView();

        public ToyView()
        {
            InitializeComponent();
            _toyViewModel = new ToyViewModel();
            DataContext = _toyViewModel;
            CbActive.IsChecked = true;
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            _toyViewModel.Query(TxbSearch.Text);
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            if (_validationView.ValidatePerson(TxBName.Text, MtxbPhone.Text, MtxbCPF.Text))
            {
                if (_toyViewModel.Save())
                {
                    System.Windows.MessageBox.Show("Toy was save!", "Save");
                }
                else
                    System.Windows.MessageBox.Show("Toy wasn't save", "Warning");
            }
        }

        private void DgToys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgToys.Items.IndexOf(DgToys.CurrentItem) >= 0)
                _toyViewModel.Select(DgToys.Items.IndexOf(DgToys.CurrentItem));
        }

        private void BtNewUser_Click(object sender, RoutedEventArgs e)
        {
            _toyViewModel.ClearView();
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

