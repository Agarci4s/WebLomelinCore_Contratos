using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System.ComponentModel;
using WebLomelinCore.Data;
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

        //Insert & update PagosAgua
        public static bool InsertaPagosAgua(pagosagua pagosagua)
        {
            return new DataSelectService().InsertaPagosAgua(pagosagua);
        }
        public static bool ActualizaPagosAgua(pagosagua pagosagua)
        {
            return new DataSelectService().ActualizaPagosAgua(pagosagua);
        }

        //Insert & update PagosLuz
        public static bool InsertaPagosLuz(pagosluz pagosluz)
        {
            return new DataSelectService().InsertaPagosLuz(pagosluz);
        }
        public static bool ActualizaPagosLuz(pagosluz pagosluz)
        {
            return new DataSelectService().ActualizaPagosLuz(pagosluz);
        }

        //Insert & update PagosPredial
        public static bool InsertaPagosPredial(pagospredial pagospredial)
        {
            return new DataSelectService().InsertaPagosPredial(pagospredial);
        }
        public static bool ActualizaPagosPredial(pagospredial pagospredial)
        {
            return new DataSelectService().ActualizaPagosPredial(pagospredial);
        }
    }
}
