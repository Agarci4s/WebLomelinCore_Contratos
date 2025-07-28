using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebLomelinCore.Models
{
    public class PagosServicios
    {
        public pagosagua PagosAgua { get; set; }
        public pagosluz PagosLuz { get; set; }
        public pagospredial PagosPredial { get; set; }

        public int IdTipoServicio { get; set; }
        [DisplayName("Servicio: ")]
        public string TipoServicio { get; set; }

        public List<SelectListItem> getTipoSerivcios { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value="0", Text = "Seleccione..", Selected=true },
            new SelectListItem {Value="1", Text = "Agua" },
            new SelectListItem { Value = "2", Text = "Luz"  },
            new SelectListItem { Value = "3", Text = "Predial"  }
        };

       

    }
}
