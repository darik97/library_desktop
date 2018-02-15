using Library.Infrastructure;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Library.ViewModel
{
    public class BookInfoViewModel : ObservableObject
    {
        public Book BookInfo { get; set; }
        public BitmapImage Image { get; set; }
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
                Image = ImageUtility.LoadImage(BookInfo.Image);
                if (Image != null)
                    return;
            }
            Image = new BitmapImage(new Uri(
                "pack://application:,,,/Resource/default_image.jpg",
                UriKind.RelativeOrAbsolute));
            Status = "Планы";
        }
    }
}
