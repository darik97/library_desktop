using System.Collections.Generic;

namespace Library.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string Link { get; set; }
        public string Series { get; set; }
        public string Status { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Author> Authors { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
