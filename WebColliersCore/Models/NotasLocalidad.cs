using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    public class NotasLocalidad
    {
        public int IdLocalidad { get; set; }
        public int IdNota { get; set; }
        public DateTime Fecha { get; set; }
        public string Nota { get; set; }
        public string Tipo { get; set; }
        public int Status { get; set; }
    }
}
