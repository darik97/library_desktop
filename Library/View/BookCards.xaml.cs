using Library.Infrastructure;
using Library.Model;
using Library.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
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
    }
}
