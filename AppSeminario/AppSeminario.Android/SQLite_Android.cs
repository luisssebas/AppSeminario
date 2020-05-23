using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppSeminario.Droid;
using AppSeminario.Interface;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace AppSeminario.Droid
{
    public class SQLite_Android : ISQLite
    {
        private SQLiteConnectionWithLock _conn;

        public SQLite_Android()
        {

        }

        private static string GetDatabasePath()
        {
            const string sqliteFilename = "Seminario.db";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);

            return path;
        }

        public SQLiteConnection GetConnection()
        {
            var dbPath = GetDatabasePath();

            // Return the synchronous database connection 
            return new SQLiteConnection(dbPath);
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var dbPath = GetDatabasePath();
            return new SQLiteAsyncConnection(dbPath);
        }

        public void DeleteDatabase()
        {
            var path = GetDatabasePath();

            try
            {
                if (_conn != null)
                {
                    _conn.Close();

                }
            }
            catch
            {
                // Best effort close. No need to worry if throws an exception
            }

            if (File.Exists(path))
            {

                File.Delete(path);
            }

            _conn = null;
        }

        public void CloseConnection()
        {
            if (_conn != null)
            {
                _conn.Close();
                _conn.Dispose();
                _conn = null;

                // Must be called as the disposal of the connection is not released until the GC runs.
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}