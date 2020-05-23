using AppSeminario.Models.Sqlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AppSeminario.Servicio
{
    public class ServicioSqlite
    {
        SQLiteAsyncConnection conn;
        //private SQLiteAsyncConnection Database { get; } = DependencyService.Get<ISQLite>().GetAsyncConnection();

        public ServicioSqlite()
        {
            var ruta = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Seminario.db");
            conn = new SQLiteAsyncConnection(ruta);
        }

        public async Task AgregarPersona(Persona dato)
        {
            await conn.InsertAsync(dato);
        }

        public async Task ActualizarPersona(Persona dato)
        {
            await conn.UpdateAsync(dato);
        }

        public async Task<List<Persona>> ObtenerPersonas()
        {
            return await conn.Table<Persona>().ToListAsync();
        }

        public async Task<int> EliminarCatalogos(Persona dato)
        {
            return await conn.DeleteAsync<Persona>(dato);
        }
    }
}
