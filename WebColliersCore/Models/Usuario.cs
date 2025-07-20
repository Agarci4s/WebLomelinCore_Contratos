using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebColliersCore.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Agregue usuario")]
        [Display(Name = "Usuario")]
        [RegularExpression("[A-Za-z\\d]+", ErrorMessage = "El usuario debe incluir sólo letras y números")]
        [MaxLength(8, ErrorMessage = "El campo es demasiado largo")]
        [MinLength(5, ErrorMessage = "El campo es demasiado pequeño")]

        public string Username { get; set; }

        [Required(ErrorMessage = "Agregue nombre")]
        [Display(Name = "Nombre")]
        [MaxLength(30, ErrorMessage = "El nombre es muy largo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Agregue apellido")]
        [Display(Name = "Primer apellido")]
        [MaxLength(30, ErrorMessage = "El apellido es muy largo")]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo apellido")]
        [MaxLength(30, ErrorMessage = "El apellido es muy largo")]
        public string Apellido2 { get; set; }

        [Required(ErrorMessage = "Agregue contraseña")]
        [Display(Name = "Contraseña")]
        [RegularExpression("[A-Za-z\\d$@.!%*?&]+", ErrorMessage = "La contraseña sólo acepta caracteres especiales(@.!%*?&), letras y dígitos")]
        [MaxLength(10, ErrorMessage = "El campo es demasiado largo")]
        [MinLength(5, ErrorMessage = "El campo es demasiado pequeño")]

        public string Password { get; set; }

        [Required(ErrorMessage = "Agregue un nivel")]
        public int Nivel { get; set; }


        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Agregue un nivel")]
        public string Rol { get; set; }

        public int Status { get; set; }
        public int StatusContratos { get; set; }

        [Required(ErrorMessage = "Agregue puesto")]
        [MaxLength(30, ErrorMessage = "El puesto es muy largo")]
        public string Puesto { get; set; }

        [Required(ErrorMessage = "Agregue teléfono")]
        [Display(Name = "Teléfono")]
        [MaxLength(30, ErrorMessage = "El teléfono es muy largo")]
        [Phone(ErrorMessage = "El formato es incorrecto")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Agregue e-mail")]
        [Display(Name = "e-mail")]
        [MaxLength(70, ErrorMessage = "El correo es muy largo")]
        [EmailAddress(ErrorMessage ="El formato es incorrecto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Agregue cancelar CFDI")]
        [Display(Name = "Cancelar CFDI")]
        public int CancelarCFDI { get; set; }

        [Required(ErrorMessage = "Agregue cancelar CFDI")]
        [Display(Name = "Cancelar CFDI")]
        public bool CancelarCFDIBool { get; set; }

        [Required(ErrorMessage = "Agregue la clave de ejecutivo")]
        [Display(Name = "Clave ejecutivo")]
        [MaxLength(10, ErrorMessage = "El correo es muy largo")]
        public string clave_ejecutivo { get; set; }
        


        public DateTime DateCreate { get; set; }

        public DateTime DateEdit { get; set; }

        public List<TpCartera> listTpCarteras { get; set; }
        public List<Transf_Opciones> listTransf_Opciones { get; set; }

        public int idCartera { get; set; }

    }
}
