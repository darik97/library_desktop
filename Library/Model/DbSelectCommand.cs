using Library.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Library.Model
{
    public class DbSelectCommand
    {
        public static Book GetBook(int id)
        {
            var query = @"SELECT Books.ID, Books.Title, Books.Image, States.Status, Authors.Name,
                        Series.Name, Books.Rating, Books.Comment, Books.Link, Genres.Genre,
                        Authors.ID, Genres.ID
                        FROM Books, States, Authors, BookAuthors, Series, Genres, BookGenres
                        WHERE Books.Status = States.ID and Books.ID = BookAuthors.Book 
                        and BookAuthors.Author = Authors.ID and Genres.ID = BookGenres.Genre
                        and Books.ID = BookGenres.Book and Books.Series = Series.ID
                        and Books.ID = " + id;
            var resultTable = DbConnection.Select(query);
            var book = new Book();

            if (resultTable.Rows.Count > 0)
            {
                book.Id = (int)resultTable.Rows[0].ItemArray[0];
                book.Title = (string)resultTable.Rows[0].ItemArray[1];
                book.Image = (byte[])resultTable.Rows[0].ItemArray[2];
                book.Status = (string)resultTable.Rows[0].ItemArray[3];
                book.Authors = new List<Author>()
                {
                    new Author {
                        Id = (int)resultTable.Rows[0].ItemArray[10],
                        Name = (string)resultTable.Rows[0].ItemArray[4]
                    }
                };
                book.Series = (string)resultTable.Rows[0].ItemArray[5];
                book.Rating = (int)resultTable.Rows[0].ItemArray[6];
                book.Comment = (string)resultTable.Rows[0].ItemArray[7];
                book.Link = (string)resultTable.Rows[0].ItemArray[8];
                book.Genres = new List<Genre>()
                {
                    new Genre {
                        Id = (int)resultTable.Rows[0].ItemArray[11],
                        Name = (string)resultTable.Rows[0].ItemArray[9]
                    }
                };

                for (int i = 1; i < resultTable.Rows.Count; i++)
                {
                    var newAuthor = new Author
                    {
                        Id = (int)resultTable.Rows[i].ItemArray[10],
                        Name = (string)resultTable.Rows[i].ItemArray[4]
                    };
                    var newGenre = new Genre
                    {
                        Id = (int)resultTable.Rows[i].ItemArray[11],
                        Name = (string)resultTable.Rows[i].ItemArray[9]
                    };
                    if (!book.Authors.Contains(newAuthor))
                        book.Authors.Add(newAuthor);
                    if (!book.Genres.Contains(newGenre))
                        book.Genres.Add(newGenre);
                }
            }
            else
            {
                //error
            }

            return book;
        }
        
        public static List<Book> FindBook(string title, string author)
        {
            var condition = string.Format(" and Books.Title LIKE '%{0}%' and Author.Name LIKE '%{1}%'", 
                title, author);
            return GetBriefBooks(condition);
        }

        public static List<Book> GetBooksWithTitle(string title)
        {
            var condition = string.Format(" and Books.Title LIKE '%{0}%'", title);
            return GetBriefBooks(condition);
        }

        public static List<Book> GetBooksWithGenre(string genre)
        {
            var condition = string.Format(" and Genre.Name LIKE '%{0}%'", genre);
            return GetBriefBooks(condition);
        }

        public static List<Book> GetBooksWithAuthor(string author)
        {
            var condition = string.Format(" and Authors.Name LIKE '%{0}%'", author);
            return GetBriefBooks(condition);
        }

        public static List<Book> GetBooksWithStatus(string status)
        {
            var condition = string.Format(" and States.Status LIKE '%{0}%'", status);
            return GetBriefBooks(condition);
        }

        public static List<Book> GetBriefBooks(string condition = "")
        {
            var query = @"SELECT Books.ID, Books.Title, Books.Image, States.Status, Authors.Name, Authors.ID
                        FROM Books, States, Authors, BookAuthors
                        WHERE Books.Status = States.ID and Books.ID = BookAuthors.Book 
                        and BookAuthors.Author = Authors.ID" + condition;
            var resultTable = DbConnection.Select(query);
            var bookDictionary = new Dictionary<int, Book>();
            if (resultTable.Rows.Count > 0)
            {
                for (int i = 0; i < resultTable.Rows.Count; i++)
                {
                    if (!bookDictionary.ContainsKey((int)resultTable.Rows[i].ItemArray[0]))
                        bookDictionary.Add((int)resultTable.Rows[i].ItemArray[0],
                            new Book
                            {
                                Id = (int)resultTable.Rows[i].ItemArray[0],
                                Title = (string)resultTable.Rows[i].ItemArray[1],
                                Image = (byte[])resultTable.Rows[i].ItemArray[2],
                                Status = (string)resultTable.Rows[i].ItemArray[3],
                                Authors = new List<Author>
                                {
                                    new Author {
                                        Id = (int)resultTable.Rows[i].ItemArray[5],
                                        Name = (string)resultTable.Rows[i].ItemArray[4]
                                    }
                                }
                            });
                    else
                    {
                        bookDictionary[(int)resultTable.Rows[i].ItemArray[0]].Authors
                            .Add(new Author
                            {
                                Id = (int)resultTable.Rows[i].ItemArray[5],
                                Name = (string)resultTable.Rows[i].ItemArray[4]
                            });
                    }
                }
            }
            return bookDictionary.Values.ToList();
        }

        public static List<Author> GetAuthorsLike(string name)
        {
            var condition = string.Format("WHERE Authors.Name LIKE '%{0}%'", name);
            return GetAllAuthors(condition);
        }

        public static List<Author> GetAllAuthors(string condition = "")
        {
            var query = @"SELECT Authors.ID, Authors.Name
                        FROM Authors" + condition;
            var resultTable = DbConnection.Select(query);
            var authors = new List<Author>();
            if (resultTable.Rows.Count > 0)
                for (int i = 0; i < resultTable.Rows.Count; i++)
                    authors.Add(
                        new Author {
                            Id = (int)resultTable.Rows[i].ItemArray[0],
                            Name = (string)resultTable.Rows[i].ItemArray[1]
                            });
            return authors;
        }

        public static List<Genre> GetGenresLike(string genre)
        {
            var condition = string.Format("WHERE Genres.Genre LIKE '%{0}%'", genre);
            return GetAllGenres(condition);
        }

        public static List<Genre> GetAllGenres(string condition)
        {
            var query = @"SELECT Genres.ID, Genres.Genre
                        FROM Genres" + condition;
            var resultTable = DbConnection.Select(query);
            var genres = new List<Genre>();
            if (resultTable.Rows.Count > 0)
                for (int i = 0; i < resultTable.Rows.Count; i++)
                    genres.Add(
                        new Genre
                        {
                            Id = (int)resultTable.Rows[i].ItemArray[0],
                            Name = (string)resultTable.Rows[i].ItemArray[1]
                        });
            return genres;
        }

        internal static List<string> GetSeriesLike(string name)
        {
            var query = @"SELECT Series.Name
                        FROM Series
                        WHERE Series.Name LIKE '%" + name + "%'";
            var resultTable = DbConnection.Select(query);
            var series = new List<string>();
            if (resultTable.Rows.Count > 0)
                for (int i = 0; i < resultTable.Rows.Count; i++)
                    series.Add((string)resultTable.Rows[i].ItemArray[0]);
            return series;
        }

        internal static List<string> GetStatesLike(string name)
        {
            var condition = "WHERE States.Status LIKE '%" + name + "%'";
            return GetStates(condition);
        }

        internal static List<string> GetStates(string condition = "")
        {
            var query = @"SELECT States.Status, States.ID
                        FROM States" + condition;
                        
            var resultTable = DbConnection.Select(query);
            var states = new List<string>();
            if (resultTable.Rows.Count > 0)
                for (int i = 0; i < resultTable.Rows.Count; i++)
                {
                    states.Add((string)resultTable.Rows[i].ItemArray[0]);
                }
            return states;
        }
    }
}
