using Library.Infrastructure;
using System.Data.SQLite;
using System.IO;
using System.Windows.Input;
using System.Data.Common;
using System.Data;

namespace Library.Infrastructure
{
    public class DbConnection : ObservableObject
    {
        #region Fields
        private static string dbFileName;
        private static SQLiteConnection dbConnection;
        private static SQLiteCommand sqlQuery;
        #endregion

        #region DMLCommands
        public static void Update(string query)
        {
            if (dbConnection.State != ConnectionState.Open)
                ConnectDb();

            try
            {
                sqlQuery.CommandText = query;
                sqlQuery.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static DataTable Select(string query)
        {
            DataTable dTable = new DataTable();

            if (dbConnection.State != ConnectionState.Open)
                ConnectDb();
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, dbConnection);
                adapter.Fill(dTable);
            }
            catch (SQLiteException ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            return dTable;
        }
        #endregion

        #region DBConnectionCommands
        public static void ConnectDb()
        {
            dbConnection = new SQLiteConnection();
            sqlQuery = new SQLiteCommand();
            dbFileName = "library.sqlite";

            if (!File.Exists(dbFileName))
                createDb();

            try
            {
                dbConnection = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                dbConnection.Open();
                sqlQuery.Connection = dbConnection;
            }
            catch (SQLiteException ex)
            {
                //Console.WriteLine("Disconnected");
                //Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void createDb()
        {
            SQLiteConnection.CreateFile(dbFileName);

            SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
            using (SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection())
            {
                connection.ConnectionString = "Data Source = " + dbFileName;
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Series (
                    ID integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                    Name varchar(100) NOT NULL UNIQUE
                    );";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS States (
                    ID integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                    Status varchar(50) NOT NULL UNIQUE
                    );";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Books (
                    ID integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                    Title varchar(100) NOT NULL,
                    Image blob,
                    Status integer NOT NULL,
                    Series integer,
                    Rating integer,
                    Comment text,
                    Link varchar(100),
                    FOREIGN KEY (Status) REFERENCES States(ID) ON DELETE CASCADE,
                    FOREIGN KEY (Series) REFERENCES Series(ID) ON DELETE CASCADE
                    );";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Genres (
                    ID integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                    Genre varchar(100) NOT NULL UNIQUE
                    );";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS BookGenres (
                    Book integer NOT NULL,
                    Genre integer NOT NULL,
                    FOREIGN KEY (Book) REFERENCES Books(ID) ON DELETE CASCADE,
                    FOREIGN KEY (Genre) REFERENCES Genres(ID) ON DELETE CASCADE,
                    PRIMARY KEY (Book, Genre)
                    );";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Authors (
                    ID integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                    Name varchar(50) NOT NULL
                    );";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS BookAuthors (
                    Book integer NOT NULL,
                    Author integer NOT NULL,
                    FOREIGN KEY (Book) REFERENCES Books(ID) ON DELETE CASCADE,
                    FOREIGN KEY (Author) REFERENCES Authors(ID) ON DELETE CASCADE,
                    PRIMARY KEY (Book, Author)
                    );";
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void disconnectDb()
        {
            if (dbConnection != null)
            {
                try
                {
                    dbConnection.Close();
                }
                catch (SQLiteException ex)
                {

                }
                finally
                {
                    dbConnection.Dispose();
                }
            }
        }
        #endregion
    }
}
