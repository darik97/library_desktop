using Library.Infrastructure;

namespace Library.Model
{
    class DbInsertCommand
    {
        //TODO: проверки на логичность, существование. exepcions
        public static void AddBook(Book book)
        {
            if (IsBookWithTitleExist(book.Title))
                return;
            if (book.Genres != null && book.Genres.Count > 0)
            {
                foreach (var genre in book.Genres)
                    if (!IsGenreExist(genre.Name))
                        AddGenre(genre.Name);
            }
            if (book.Authors != null && book.Authors.Count > 0)
            {
                foreach (var author in book.Authors)
                    if (!IsGenreExist(author.Name))
                        AddAuthor(author.Name);
            }
            if (book.Series != null && !IsSeriesExist(book.Series))
                AddSeries(book.Series);
            if (!IsStatusExist(book.Status))
                AddStatus(book.Status);

            insertBook(book);
        }

        private static void insertBook(Book book)
        {
            var query = string.Format(
                @"INSERT INTO Books ('Title', 'Image', 'Series', 'Status', 'Rating', 'Comment', 'Link')
                values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                book.Title, book.Image, DbSelectCommand.GetSeriesLike(book.Series)[0],
                DbSelectCommand.GetStatesLike(book.Status)[0], book.Rating, book.Comment, book.Link);
        }

        private static void insertBookShort(Book book)
        {
            var query = string.Format(
                @"INSERT INTO Books ('Title', 'Image', 'Status')
                values ('{0}', '{1}', '{2}')",
                book.Title, book.Image, DbSelectCommand.GetStatesLike(book.Status)[0]);
        }

        public static void AddAuthor(string name)
        {
            var query = string.Format("INSERT INTO Authors ('Name') values ('{0}')", name);
            DbConnection.Update(query);
        }

        public static void AddStatus(string status)
        {
            var query = string.Format("INSERT INTO States ('Status') values ('{0}')", status);
            DbConnection.Update(query);
        }

        public static void AddSeries(string series)
        {
            var query = string.Format("INSERT INTO Series ('Name') values ('{0}')", series);
            DbConnection.Update(query);
        }

        public static void AddGenre(string genre)
        {
            var query = string.Format("INSERT INTO Genres ('Genre') values ('{0}')", genre);
            DbConnection.Update(query);
        }

        //TODO: make multiple authors and genres
        public static void PinnAuthorToBook(Book book, Author author)
        {
            var query = string.Format("INSERT INTO BookAuthors ('Book', 'Author') values ('{0}', '{1}')", 
                DbSelectCommand.GetBooksWithTitle(book.Title)[0].Id,
                DbSelectCommand.GetAuthorsLike(author.Name)[0]);
            DbConnection.Update(query);
        }

        public static void PinnGenreToBook(Book book, Genre genre)
        {
            var query = string.Format("INSERT INTO BookGenres ('Book', 'Genre') values ('{0}', '{1}')",
                DbSelectCommand.GetBooksWithTitle(book.Title)[0].Id,
                DbSelectCommand.GetGenresLike(genre.Name)[0]);
            DbConnection.Update(query);
        }

        public static bool IsAuthorExist(string param)
        {
            if (DbSelectCommand.GetAuthorsLike(param).Count > 0)
                return true;
            return false;
        }

        public static bool IsGenreExist(string param)
        {
            if (DbSelectCommand.GetGenresLike(param).Count > 0)
                return true;
            return false;
        }

        public static bool IsSeriesExist(string param)
        {
            if (DbSelectCommand.GetSeriesLike(param).Count > 0)
                return true;
            return false;
        }

        public static bool IsStatusExist(string param)
        {
            if (DbSelectCommand.GetStatesLike(param).Count > 0)
                return true;
            return false;
        }

        public static bool IsBookExist(int id)
        {
            if (DbSelectCommand.GetBook(id) != null)
                return true;
            return false;
        }

        public static bool IsBookExist(string title, string author)
        {
            if (DbSelectCommand.FindBook(title, author).Count > 0)
                return true;
            return false;
        }

        public static bool IsBookWithTitleExist(string title)
        {
            if (DbSelectCommand.FindBookWithTitle(title).Count > 0)
                return true;
            return false;
        }
    }
}
