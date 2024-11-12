using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Registro_Escuela.Models
{
	public class Registro
	{
        public int ID_Usuario { get; set; }
        [Required(ErrorMessage ="Campo Obligatorio")]
        public string? Nombres { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string? Apellidos { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string? NomTutor { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string? Contraseña { get; set; }


    



    }
}
