using Library.Infrastructure;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Library.ViewModel
{
    public class BookInfoViewModel : ObservableObject
    {
        public Book BookInfo { get; set; }
        public List<string> States { get; set; }
        public string Status { get; set; }
        public List<int> RatingRange { get; set; }
        public bool IsExist { get; set; }

        public BookInfoViewModel(bool isExist, int id)
        {
            IsExist = isExist;
            States = DbSelectCommand.GetStates();
            RatingRange = new List<int>();
            for (int i = 1; i <= 10; i++)
                RatingRange.Add(i);

            if (IsExist)
            {
                BookInfo = DbSelectCommand.GetBook(id);
                Status = BookInfo.Status;
                if (BookInfo.Image != null)
                    return;
            }
            else
            {
                BookInfo = new Book()
                {
                    Title = "",
                    Authors = new List<Author>() { new Author() },
                    Series = "",
                    Genres = new List<Genre>() { new Genre() },
                    Comment = "",
                    Link = "",
                    Rating = 0, 
                    Status = "Планы"
                };
            }
            BookInfo.Image = ImageUtility.ImageSourceToBytes(
                new JpegBitmapEncoder(), 
                new BitmapImage(new Uri(
                "pack://application:,,,/Resource/default_image.jpg",
                UriKind.RelativeOrAbsolute)));
        }

        public ICommand BackCommand
        {
            get
            {
                return new DelegateCommand(o => MainWindowViewModel.OpenAllBooks());
            }
        }

        public void SaveBook(bool isImageChanged, ImageSource imageSource)
        {
            if (isImageChanged)
                BookInfo.Image = ImageUtility.ImageSourceToBytes(
                    new JpegBitmapEncoder(), imageSource);
            if (IsExist)
            {
                MessageBox.Show("обновить книгу");
                //DbDeleteUpdateCommand.UpdateBook(BookInfo);
                return;
            }
            MessageBox.Show("создать книгу");
            //DbInsertCommand.AddBook(BookInfo);
        }

        internal void Delete()
        {
            MessageBox.Show("удалить книгу");
            //DbDeleteUpdateCommand.DeleteBook(BookInfo.Id);
        }
    }
}
