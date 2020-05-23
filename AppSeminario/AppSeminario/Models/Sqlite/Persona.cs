using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSeminario.Models.Sqlite
{
    public class Persona
    {
        [PrimaryKey, AutoIncrement]
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public string Carrera { get; set; }
    }
}
