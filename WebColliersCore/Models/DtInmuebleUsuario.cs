using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    public class DtInmuebleUsuario
    {
        //[DefaultValue(true)]   //[Required(ErrorMessage = "Agregue antenombre")]
        //[Display(Name = "Antenombre")]
        //[MaxLength(25, ErrorMessage = " ")]
        public Int64 idInmueble { get; set; }
        public Int64 Administrativo { get; set; }
        public string NombreInmueble { get; set; }
        public string Calle { get; set; }//100
        public string Colonia { get; set; }//100
        public string CP { get; set; }//5
        public Int64 IdCartera { get; set; }
        public Int64 IdInmuebleUsuario { get; set; }
        public Int64 IdUsuario { get; set; }
        public string Propietario { get; set; }
        public bool checkAux { get; set; }
    }
}
