using Library.Infrastructure;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Library.ViewModel
{
    public class BookCardsViewModel : ObservableObject
    {
        public List<Book> Books { get; set; }

        public BookCardsViewModel(Content content, string parameter = null)
        {
            switch (content)
            {
                case Content.All:
                    //Books = DbSelectCommand.GetBooksWithStatus("Прочитано");
                    Books = new List<Book>()
                    {
                        new Book() {
                            Id = 0, Title = "FirstBook",
                            Authors = new List<Author>() {
                                new Author() {Id = 0, Name = "author one"}
                            },
                            Image = ImageUtility.ImageSourceToBytes(
                                new JpegBitmapEncoder(),
                                new BitmapImage(new Uri(
                                "pack://application:,,,/Resource/default_image.jpg",
                                UriKind.RelativeOrAbsolute))),
                            Status = "Прочитано"
                        },
                        new Book() {
                            Id = 1, Title = "SecondBook",
                            Authors = new List<Author>() {
                                new Author() {Id = 1, Name = "author two"}
                            },
                            Status = "Прочитано"
                        },
                    };
                    break;
                case Content.StatusRead:
                    //Books = DbSelectCommand.GetBooksWithStatus("Прочитано");
                    Books = new List<Book>()
                    {
                        new Book() {
                            Id = 0, Title = "FirstBookRead",
                            Authors = new List<Author>() {
                                new Author() {Id = 0, Name = "author one"}
                            },
                            Image = ImageUtility.ImageSourceToBytes(
                                new JpegBitmapEncoder(),
                                new BitmapImage(new Uri(
                                "pack://application:,,,/Resource/default_image.jpg",
                                UriKind.RelativeOrAbsolute))),
                            Status = "Прочитано"
                        },
                        new Book() {
                            Id = 1, Title = "SecondBookRead",
                            Authors = new List<Author>() {
                                new Author() {Id = 1, Name = "author two"}
                            },
                            Status = "Прочитано"
                        },
                    };
                    break;
                case Content.StatusPlans:
                    //Books = DbSelectCommand.GetBooksWithStatus("Планы");
                    Books = new List<Book>()
                    {
                        new Book() {
                            Id = 0, Title = "FirstBookPlan",
                            Authors = new List<Author>() {
                                new Author() {Id = 0, Name = "author one"}
                            },
                            Image = ImageUtility.ImageSourceToBytes(
                                new JpegBitmapEncoder(),
                                new BitmapImage(new Uri(
                                "pack://application:,,,/Resource/default_image.jpg",
                                UriKind.RelativeOrAbsolute))),
                            Status = "Прочитано"
                        },
                        new Book() {
                            Id = 1, Title = "SecondBookPlan",
                            Authors = new List<Author>() {
                                new Author() {Id = 1, Name = "author two"}
                            },
                            Status = "Прочитано"
                        },
                    };
                    break;
                case Content.ByAuthor:
                    //Books = DbSelectCommand.GetBooksWithStatus("Планы");
                    Books = new List<Book>()
                    {
                        new Book() {
                            Id = 0, Title = "FirstBookAuthor",
                            Authors = new List<Author>() {
                                new Author() {Id = 0, Name = "author one"}
                            },
                            Image = ImageUtility.ImageSourceToBytes(
                                new JpegBitmapEncoder(),
                                new BitmapImage(new Uri(
                                "pack://application:,,,/Resource/default_image.jpg",
                                UriKind.RelativeOrAbsolute))),
                            Status = "Прочитано"
                        },
                        new Book() {
                            Id = 1, Title = "SecondBookAuthor",
                            Authors = new List<Author>() {
                                new Author() {Id = 1, Name = "author two"}
                            },
                            Status = "Прочитано"
                        },
                    };
                    break;

                    //    //TODO: Content.ByAuthor, Content.Genre
            }
        }

        #region
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

        public ICommand GetBooksByAuthorCommand
        {
            get
            {
                return new DelegateCommand(o => getBooksByAuthor((string)o));
            }
        }

        public ICommand CreateNewBookCommand
        {
            get
            {
                return new DelegateCommand(o => MainWindowViewModel.CreateNewBook());
            }
        }

        public ICommand OpenBooksByAuthorCommand
        {
            get
            {
                return new DelegateCommand(o => MainWindowViewModel.OpenAllBooks(Content.ByAuthor));
            }
        }
        #endregion

        private void getBooksByAuthor(string author)
        {
            //Books = DbSelectCommand.GetBooksWithAuthor(author);
        }

        private void getAllBooks()
        {
            //Books = DbSelectCommand.GetBriefBooks();
        }

    }
}
