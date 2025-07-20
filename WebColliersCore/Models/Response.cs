using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebColliersCore.Models
{
    [Serializable]
    /// <summary>
    /// Obtiene o estable el resultado en una clase.
    /// </summary
    public class Response<TClass> : Status
    {

        public TClass Resultado { get; set; }


    }
}
