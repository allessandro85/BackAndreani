using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Usuario
    {
        public int id_usuario { get; set; }

        public int id_persona { get; set; }

        public string? nombre { get; set; }

        public string? apellido { get; set; }

        public int documento { get; set; }

        public string? Email { get; set; }

        public string? Telefono { get; set; }

    }
}
