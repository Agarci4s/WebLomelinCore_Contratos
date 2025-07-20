using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    public class Transf_Opciones
    {
        public int idTransfOpciones { get; set; }
        public int opcion { get; set; }
        public int sub { get; set; }
        public int sub_sub { get; set; }
        public string descripcion { get; set; }
        public string NameOpcion { get; set; }
        public string NameSub { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int TipoVista { get; set; }
        public bool checkTransf_Opciones { get; set; }
        public int Nivel { get; set; }
        public string NivelStr { get; set; }

    }
}
