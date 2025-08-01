using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebLomelinCore.Data;

namespace WebLomelinCore.Models
{
    public class PagosServicios
    {
        public pagosagua PagosAgua { get; set; }
        public pagosluz PagosLuz { get; set; }
        public pagospredial PagosPredial { get; set; }
        public int TipoServicio { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectListItem> getTipoSerivcios             
        {
            get {
                return new DataSelectService().getTipoServicio.OrderBy(x => x.Value).ToList();
            }
        }

    }
}
