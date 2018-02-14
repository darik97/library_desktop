using Library.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Library.View
{
    /// <summary>
    /// Логика взаимодействия для Book.xaml
    /// </summary>
    public partial class BookInfo : UserControl
    {
        public BookInfo()
        {
            InitializeComponent();
            DataContext = new BookInfoViewModel();
            bookImage.Source = new BitmapImage(
                new Uri("pack://application:,,,/Resource/default_image.jpg", UriKind.RelativeOrAbsolute));
            var ratingRange = new List<int>();
            for (int i = 1; i <= 10; i++)
                ratingRange.Add(i);
            ratingComboBox.ItemsSource = ratingRange;
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
