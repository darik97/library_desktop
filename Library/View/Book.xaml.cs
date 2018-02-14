using Library.ViewModel;
using Microsoft.Win32;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Library.View
{
    /// <summary>
    /// Логика взаимодействия для Book.xaml
    /// </summary>
    public partial class Book : UserControl
    {
        public Book()
        {
            InitializeComponent();
            DataContext = new BookViewModel();
            bookImage.Source = new BitmapImage(
                new Uri("pack://application:,,,/Resource/default_image.jpg", UriKind.RelativeOrAbsolute));
        }

        private void AddPhoto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                bookImage.Source = new BitmapImage(new Uri(op.FileName));
            }
        }
    }
}
