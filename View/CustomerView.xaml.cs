using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToyStore.ViewModel;
using Xceed.Wpf.Toolkit;

namespace ToyStore.View
{
    /// <summary>
    /// Interação lógica para CustomerView.xam
    /// </summary>
    public partial class CustomerView : Page
    {
        private CustomerViewModel _customerViewModel { get; set; }

        public CustomerView()
        {
            InitializeComponent();
            _customerViewModel = new CustomerViewModel();
            DataContext = _customerViewModel;
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
