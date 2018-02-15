using Library.Infrastructure;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModel
{
    public class ListPageViewModel : ObservableObject
    {
        public bool IsAboutAuthor { get; set; }
        public IEnumerable<string> Data { get; set; }

        public ListPageViewModel(bool isAboutAuthor)
        {
            IsAboutAuthor = isAboutAuthor;
            if (isAboutAuthor)
            {
                Data = new List<string>();
            }
        }
    }
}
