using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebColliersCore.DataAccess;

namespace WebLomelinCore.Data
{
    public class DataSelectService
    {
        private Conexion conexion = new Conexion();

        public List<SelectListItem> getCamposStatusServicio { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="1", Text = "Autorizado" },
            new SelectListItem { Value = "2", Text = "Rechazado"  },
            new SelectListItem { Value = "3", Text = "Pagado"  },
            new SelectListItem { Value = "4", Text = "Cargado"  }
        };

        public List<SelectListItem> getCamposTipoServicio { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="1", Text = "Agua" },
            new SelectListItem { Value = "2", Text = "Luz"  },
            new SelectListItem{ Value = "3", Text = "Predial"  }
        };   
    }
}
