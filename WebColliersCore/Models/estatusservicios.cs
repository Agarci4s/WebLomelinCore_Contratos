using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebLomelinCore.Models
{
    public class estatusservicios
    {
        public int Id { get; set; }
        public string Estatus { get; set; }
        public int Orden { get; set; }
        public int PerfilGestor { get; set; }
        public int PerfilEjecutivo { get; set; }
        public int PerfilDirectorInm { get; set; }
        public int PerfilAdmin { get; set; }
        public int PerfilTesoreria { get; set; }
        public int PerfilRecepcion { get; set; }
        public int Status { get; set; }

        public List<SelectListItem> GetEstatusservicios
        {
            get
            {
                List<estatusservicios> estatuslist =
                new List<estatusservicios>
                {
                    new estatusservicios { Id = 1, Estatus = "Cargado" },
                    new estatusservicios { Id = 2, Estatus = "Aprobado" },
                    new estatusservicios { Id = 3, Estatus = "Rechazado" },
                };

                List<SelectListItem> response = new List<SelectListItem>();
                foreach (var item in estatuslist)
                {
                    response.Add(new SelectListItem
                    {
                        Text = item.Estatus,
                        Value = item.Id.ToString()
                    });
                }
                return response;
            }
        }
    }
}
