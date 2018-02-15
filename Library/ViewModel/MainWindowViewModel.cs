using Library.Infrastructure;
using System.Linq;
using System.Windows;

namespace Library.ViewModel
{
    public class MainWindowViewModel
    {
        static MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

        public MainWindowViewModel()
        {
            DbConnection.ConnectDb();
            OpenAllBooks();

            OpenAllBooksCommand = new DelegateCommand(o => OpenAllBooks());

            OpenBooksInPlanCommand = new DelegateCommand(o => openBooksWithState("Прочитано"));

            OpenBooksReadCommand = new DelegateCommand(o => openBooksWithState("Планы"));

            OpenAuthorsCommand = new DelegateCommand(o => openAuthors());

            OpenGenresCommand = new DelegateCommand(o => openGenres());

            OpenPaletteCommand = new DelegateCommand(o => openPalette());

            CreateNewBookCommand = new DelegateCommand(o => CreateNewBook());

            OpenBookCommand = new DelegateCommand(o => OpenBook((int)o));
        }

        #region Command
        public DelegateCommand OpenAllBooksCommand { get; set; }

        public DelegateCommand OpenBooksInPlanCommand { get; set; }

        public DelegateCommand OpenBooksReadCommand { get; set; }

        public DelegateCommand OpenAuthorsCommand { get; set; }

        public DelegateCommand OpenGenresCommand { get; set; }

        public DelegateCommand OpenPaletteCommand { get; set; }

        public DelegateCommand CreateNewBookCommand { get; set; }

        public DelegateCommand OpenBookCommand { get; set; }

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
            window.mainGrid.Children.Add(new View.BookInfo(false));
        }

        public static void OpenBook(int id)
        {
            window.mainGrid.Children.Clear();
            window.mainGrid.Children.Add(new View.BookInfo(true, id));
        }

        public static void OpenAllBooks()
        {
            window.mainGrid.Children.Clear();
            window.mainGrid.Children.Add(new View.BookCards());
        }

        private void openGenres()
        {
            window.mainGrid.Children.Clear();
            window.mainGrid.Children.Add(new View.ListPage(false));
        }

        private void openAuthors()
        {
            window.mainGrid.Children.Clear();
            window.mainGrid.Children.Add(new View.ListPage(true));
        }

        private void openBooksWithState(string state)
        {
            window.mainGrid.Children.Clear();
            window.mainGrid.Children.Add(new View.BookCards());
        }

        #endregion
    }
}
