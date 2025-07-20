using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "Agregue usuario")]
        [Display(Name = "Usuario")]
        [MaxLength(8, ErrorMessage = " ")]
        [MinLength(3, ErrorMessage = "El campo es demasiado pequeño")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Agregue contraseña")]
        [Display(Name = "Contraseña")]
        [MaxLength(8, ErrorMessage = " ")]
        [MinLength(3, ErrorMessage = "El campo es demasiado pequeño")]
        public string Password { get; set; }

    }
}
