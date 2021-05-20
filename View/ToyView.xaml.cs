using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ToyStore.ViewModel;

namespace ToyStore.View
{
    public partial class ToyView : Page
    {
        private ToyViewModel _toyViewModel { get; set; }
        private readonly string _imageDefault = "..\\..\\Images\\InsertPicture.PNG";

        public ToyView()
        {
            InitializeComponent();
            _toyViewModel = new ToyViewModel();
            DataContext = _toyViewModel;
            CbActive.IsChecked = true;
            ImageDefault(_imageDefault);
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            _toyViewModel.Query(TxbSearch.Text);
        }
         
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            if (_toyViewModel.Save())
            {
                ImageDefault(_imageDefault);
                MessageBox.Show("Toy was save!", "Save");
            }

            else
                MessageBox.Show("Toy wasn't save", "Warning");
        }

        private void DgToys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgToys.Items.IndexOf(DgToys.CurrentItem) >= 0)
            {
                _toyViewModel.Select(DgToys.Items.IndexOf(DgToys.CurrentItem));
                if (_toyViewModel.Toy.Image[0] != 0)
                    ImgToy.Source = ByteToImage(_toyViewModel.Toy.Image);
                else
                    ImageDefault(_imageDefault);
            }   
        }

        private void BtNewUser_Click(object sender, RoutedEventArgs e)
        {
            _toyViewModel.ClearView();
            CbActive.IsChecked = true;
            ImageDefault(_imageDefault);
        }

        private void TxbAmountOfStock_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TxbAmountOfStock.Text = "";
        }

        private void ImageDefault(string imageSource)
        {
            ImgToy.Source = new ImageSourceConverter().ConvertFromString(imageSource) as ImageSource;
        }

        private void ImgToy_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                FileStream stream = File.OpenRead(dlg.FileName);
                _toyViewModel.Toy.Image = new byte[stream.Length];
                stream.Read(_toyViewModel.Toy.Image, 0, _toyViewModel.Toy.Image.Length);
                stream.Close();
                ImgToy.Source = ByteToImage(_toyViewModel.Toy.Image);
            }
        }

        public ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            biImg.BeginInit();
            MemoryStream ms = new MemoryStream(imageData);

            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }

        private void DgToys_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Image")
                e.Column = null;
        }
    }
}

