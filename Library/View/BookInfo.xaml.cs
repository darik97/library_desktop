using Library.Infrastructure;
using Library.Model;
using Library.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Library.View
{
    /// <summary>
    /// Логика взаимодействия для Book.xaml
    /// </summary>
    public partial class BookInfo : UserControl
    {
        bool isExist;
        BookInfoViewModel book;
        bool isImageChanged = false;

        public BookInfo(bool isExist, int id = -1)
        {
            InitializeComponent();
            this.isExist = isExist;
            book = new BookInfoViewModel(isExist, id);
            DataContext = book;
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                bookImage.Source = new BitmapImage(new Uri(op.FileName));
                isImageChanged = true;
            }
        }

        private void SaveBook_Click(object sender, RoutedEventArgs e)
        {
            if (tbTitle.Text.Trim() == "")
            {
                MessageBox.Show("Требуется ввести название книги", 
                    "Ошибка при добавлении книги",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            book.SaveBook(isImageChanged, bookImage.Source);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            book.Delete();
        }
    }
}
