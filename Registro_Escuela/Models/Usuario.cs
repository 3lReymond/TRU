using Registro_Escuela.Models;
namespace Registro_Escuela.Models
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }

        public string[] Roles { get; set; }
    }
}
