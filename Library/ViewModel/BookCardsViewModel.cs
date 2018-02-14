using Library.Helpers;
using Library.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.ViewModel
{
    public class BookCardsViewModel : ObservableObject
    {
        public IEnumerable<Book> Books { get; set; }

        public BookCardsViewModel()
        {
            Books = new List<Book>();
            getAllBooks();
        }

        public ICommand OpenBookCommand
        {
            get
            {
                return new DelegateCommand(o => MainWindowViewModel.OpenBook((int)o));
            }
        }

        public ICommand GetAllBooksCommand
        {
            get
            {
                return new DelegateCommand(o => getAllBooks());
            }
        }

        private void getAllBooks()
        {
            Books = DbSelectCommand.GetBriefBooks();
        }
        
        public ICommand GetBooksByAuthorCommand
        {
            get
            {
                return new DelegateCommand(o => getBooksByAuthor((string)o));
            }
        }

        private void getBooksByAuthor(string author)
        {
            Books = DbSelectCommand.GetBooksWithAuthor(author);
        }


        public ICommand CreateNewBookCommand
        {
            get
            {
                return new DelegateCommand(o => MainWindowViewModel.CreateNewBook());
            }
        }
    }
}
