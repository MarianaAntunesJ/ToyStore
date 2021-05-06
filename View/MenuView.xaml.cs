using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        private void hover_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
        }

        private void hover_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BtnHome.Background = Brushes.Transparent;
        }

        private void BtnWorker_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BtnHome.Background = Brushes.Coral;
        }
    }
}
