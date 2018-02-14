using Library.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    class DbDeleteUpdateCommand
    {
        public static void DeleteBook(int id)
        {
            var query = "DELETE FROM Books WHERE Books.ID = " + id;
        }

        public static void UpdateBook(Book newbook)
        {
            var query = string.Format(
                @"UPDATE Books SET Title = '{0}', Image = '{1}', 
                Status = (SELECT ID FROM States WHERE Status = '{2}'),
                Series = (SELECT ID FROM Series WHERE Name = '{3}'), 
                Rating = '{4}', Comment = '{5}', Link = '{6}'
                WHERE ID = {7}",
                newbook.Title, newbook.Image, newbook.Status, newbook.Series,
                newbook.Rating, newbook.Comment, newbook.Link, newbook.Id);
            DbConnection.Update(query);
        }

        public static void UpdateBookAuthor(Book newbook)
        {
            var query = string.Format(
                @"UPDATE BookAuthors 
                SET Author = (SELECT ID FROM Authors WHERE Name = '{0}')
                WHERE Book = {1}",
                newbook.Authors[0].Name, newbook.Id);
            DbConnection.Update(query);
        }

        public static void UpdateBookGenre(Book newbook)
        {
            var query = string.Format(
                @"UPDATE BookGenres 
                SET Genre = (SELECT ID FROM Genres WHERE Genre = '{0}')
                WHERE Book = {1}",
                newbook.Genres[0].Name, newbook.Id);
            DbConnection.Update(query);
        }
    }
}
