using System.Windows;
using System.Windows.Controls;

namespace ToyStore.View
{
    public partial class MenuView : Window
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void ControlaGridCursor(RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            GridCursor.Margin = new Thickness(10 + (150 * index), 50, 0, 0);
        }

        private void BtnClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnCustomer_Click(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new CustomerView());
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new MainView());
        }

        private void BtnPurchase_Click(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new PurchaseView());
        }

        private void BtnToy_Click(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new ToyView());
        }

        private void BtnWorker_Click(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new EmployeeView());
        }
    }
}
