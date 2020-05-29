using System;
using System.Collections.Generic;
using System.Text;

namespace AppSeminario.Entidades.Google
{
    public class legs
    {
        public List<steps> steps { get; set; }
        public ubicacion end_location { get; set; }
        public ubicacion start_location { get; set; }
    }
}
