using Library.Infrastructure;
using Library.Model;
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
        public BookInfo(bool isExist, int id = -1)
        {
            InitializeComponent();
            var book = new BookInfoViewModel(isExist, id);
            DataContext = book;
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

        private void AddBook_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var newBook = new Book();
        }
    }
}
