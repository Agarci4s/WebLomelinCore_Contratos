using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    public class Transf_Opciones_Usuarios
    {
        public Int64 idTransfOpcionesUsuarios { get; set; }
        public int IdUsuario { get; set; }
        public int idTransfOpciones { get; set; }
        public int Nivel { get; set; }
        public int TipoVista { get; set; }
    }
}
