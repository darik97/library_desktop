using Library.Helpers;
using System.Linq;
using System.Windows;
using System;
using Library.Infrastructure;

namespace Library.ViewModel
{
    public class MainWindowViewModel
    {
        static MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

        public MainWindowViewModel()
        {
            DbConnection.ConnectDb();
            openAllBooks();

            OpenAllBooksCommand = new DelegateCommand(o => openAllBooks());

            OpenBooksInPlanCommand = new DelegateCommand(o => openBooksWithState("Прочитано"));

            OpenBooksReadCommand = new DelegateCommand(o => openBooksWithState("Планы"));

            OpenAuthorsCommand = new DelegateCommand(o => openAuthors());

            OpenGenresCommand = new DelegateCommand(o => openGenres());

            OpenPaletteCommand = new DelegateCommand(o => openPalette());

            CreateNewBookCommand = new DelegateCommand(o => CreateNewBook());
        }

        internal static void OpenBook(int o)
        {
            throw new NotImplementedException();
        }

        #region Command
        public DelegateCommand OpenAllBooksCommand { get; set; }

        public DelegateCommand OpenBooksInPlanCommand { get; set; }

        public DelegateCommand OpenBooksReadCommand { get; set; }

        public DelegateCommand OpenAuthorsCommand { get; set; }

        public DelegateCommand OpenGenresCommand { get; set; }

        public DelegateCommand OpenPaletteCommand { get; set; }

        public DelegateCommand CreateNewBookCommand { get; set; }

        #endregion

        #region Command implementation

        private void openPalette()
        {
            window.mainGrid.Children.Clear();
            window.mainGrid.Children.Add(new View.Palette());
        }
        
        public static void CreateNewBook()
        {
            window.mainGrid.Children.Clear();
            window.mainGrid.Children.Add(new View.BookInfo());
        }

        private void openAllBooks()
        {
            window.mainGrid.Children.Clear();
            window.mainGrid.Children.Add(new View.BookCards());
        }

        private void openGenres()
        {
            window.mainGrid.Children.Clear();
            window.mainGrid.Children.Add(new View.BookCards());
        }

        private void openAuthors()
        {
            window.mainGrid.Children.Clear();
            window.mainGrid.Children.Add(new View.BookCards());
        }

        private void openBooksWithState(string state)
        {
            window.mainGrid.Children.Clear();
            window.mainGrid.Children.Add(new View.BookCards());
        }

        #endregion
    }
}
