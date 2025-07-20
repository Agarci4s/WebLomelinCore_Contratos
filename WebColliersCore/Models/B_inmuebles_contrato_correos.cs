using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace WebColliersCore.Models
{
    public class B_inmuebles_contrato_correos
    {
        public int id_b_inmuebles_contrato_correo { get; set; }

        public int id_b_inmuebles_contrato { get; set; }


        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(50, ErrorMessage = "Agregue un valor valido")]
        public string nombre { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [EmailAddress(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(50, ErrorMessage = "Agregue un valor valido")]
        public string correo { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        [MaxLength(50, ErrorMessage = "Agregue un valor valido")]
        public string telefono { get; set; }




    }
}
