using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebLomelinCore.Data;

using WebLomelinCore.Data;
namespace WebLomelinCore.Models
{
    public class PagosServicios
    {
        public pagosagua PagosAgua { get; set; }
        public pagosluz PagosLuz { get; set; }
        public pagospredial PagosPredial { get; set; }
        public int TipoServicio { get; set; }

        public static List<SelectListItem> setItem(List<SelectListItem> listItems, int? id)
        {
            if (id.HasValue)
            {
                foreach (SelectListItem item in listItems)
                {
                    if (item.Value == id.ToString())
                    {
                        item.Selected = true;
                    }
                }
            }
            return listItems.OrderBy(x => x.Value).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<SelectListItem> getTipoSerivcios
        {
            get
            {
                return new DataSelectService().getTipoServicio.OrderBy(x => x.Value).ToList();
            }
        }

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
