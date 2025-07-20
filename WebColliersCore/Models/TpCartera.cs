using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    public class TpCartera
    {
        public int idCartera { get; set; }
        [Display(Name = "Cartera")]
        [MaxLength(60, ErrorMessage = "Longitud máxima de 60 caracteres.")]
        public string descripcionCartera { get; set; }
        //[Required(ErrorMessage = "Falta proporcionar la letra para la facturación")]
        [Display(Name = "Letra Factura")]
        [MaxLength(1)]
        public string letra { get; set; }
        [Display(Name = "Complemento de Terceros")]
        public bool complemento_terceros { get; set; }
        public int status { get; set; }
        public bool carteraBool { get; set; }

        public List<DtInmuebleUsuario> dtInmuebleUsuarioList { get; set; }

        public RegistroSeries Registro {get;set;}
    }
}
