using System.Security.Claims;

namespace Model
{
    public class Usuario
    {
        public int id_usuario { get; set; }

        public int id_persona { get; set; }
        public string? usuario { get; set; }

        public string? nombre { get; set; }

        public string? apellido { get; set; }

        public int documento { get; set; }

        public string? email { get; set; }

        public string? telefono { get; set; }

        public static string validarToken(ClaimsIdentity identity, List<Model.Usuario> usuarios)
        {
            var respuesta = "";
            try
            {
                if (identity.Claims.Count() == 0)
                {

                    respuesta = "Verificar Token";
                }
                var id = false;
                foreach (var item in identity.Claims)
                {
                    foreach (var itemUsuario in usuarios)
                    {
                        if (item.Value == itemUsuario.usuario)
                        {
                            id = true;
                        }
                    }
                    
                }
                if (id)
                {
                    respuesta = "Usuario Correcto";
                }
                else
                {
                    respuesta = "Usuario Incorrecto";
                }
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            return respuesta;
        }
    }
}
