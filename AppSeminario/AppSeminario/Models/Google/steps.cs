using System;
using System.Collections.Generic;
using System.Text;

namespace AppSeminario.Entidades.Google
{
    public class steps
    {
        public ubicacion start_location { get; set; }
        public ubicacion end_location { get; set; }
        public polyline polyline { get; set; }
    }
}
