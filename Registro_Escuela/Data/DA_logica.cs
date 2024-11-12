using Registro_Escuela.Models;

namespace Registro_Escuela.Data
{
    public class DA_logica
    {
        public List<Usuario> ListaUsuario()
        {


            return new List<Usuario>
            {
                new Usuario{Nombre = "ramon",Correo ="ramonbatidas@gmail.com",Clave ="1234",Roles = new string[] {"Administrador"} },
                  new Usuario{Nombre = "tania",Correo ="taniaportillo@gmail.com",Clave ="1234",Roles = new string[] {"Tutor"} },

            };
        }  
        
        public Usuario ValidarUsuario(string _correo ,string _clave)
        {
            return ListaUsuario().Where(item => item.Correo == _correo && item.Clave == _clave).FirstOrDefault();
        }



    }
}
