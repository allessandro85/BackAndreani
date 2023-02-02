using System.Security.Claims;

namespace Model
{
    public class Especialidades
    {
        public int id_especialidad { get; set; }

        public int id_estado { get; set; }
        public string id_horario { get; set; }

        public string? nombre { get; set; }

        public string? codigo { get; set; }

    }
}
