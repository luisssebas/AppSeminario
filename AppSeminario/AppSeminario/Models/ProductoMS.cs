using System;
using System.Collections.Generic;
using System.Text;

namespace AppSeminario.Models
{
    public class ProductoMS
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }

        public ServicioMS Servicio { get; set; }
    }
}
