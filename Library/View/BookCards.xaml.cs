using Library.Infrastructure;
using Library.Model;
using Library.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace Library.View
{
    /// <summary>
    /// Логика взаимодействия для Books.xaml
    /// </summary>
    public partial class BookCards : UserControl
    {
        public BookCards(Content content = Model.Content.All)
        {
            InitializeComponent();
            var context = new BookCardsViewModel(content);
            DataContext = context;
        }

        private void Author_Click(object sender, RoutedEventArgs e)
        {
            var context = new BookCardsViewModel(Model.Content.ByAuthor,
                ((Hyperlink)sender).NavigateUri.ToString());
            DataContext = context;
        }

        private void Book_Click(object sender, RoutedEventArgs e)
        {
            int bookId = Convert.ToInt32(((Hyperlink)sender).NavigateUri.ToString());
            //DbSelectCommand.FindBook(((Hyperlink)sender).NavigateUri.ToString(), null);
            MainWindowViewModel.OpenBook(bookId);
        }
    }
}
