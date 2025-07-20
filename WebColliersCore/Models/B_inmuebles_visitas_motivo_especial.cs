using System.ComponentModel.DataAnnotations;

namespace WebLomelinCore.Models
{
    public class B_inmuebles_visitas_motivo_especial
    {
        public int id_b_cg_motivo_especial { get; set; }

        public bool status { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Agregue un valor valido")]
        public string descripcion { get; set; }

       
    }
}
