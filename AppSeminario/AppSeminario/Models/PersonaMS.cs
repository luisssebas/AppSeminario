using System;
using System.Collections.Generic;
using System.Text;

namespace AppSeminario.Models
{
    public class PersonaMS
    {
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public string Carrera { get; set; }
    }
}
