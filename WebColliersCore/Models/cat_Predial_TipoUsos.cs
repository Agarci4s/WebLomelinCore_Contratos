using System.ComponentModel.DataAnnotations;

namespace WebLomelinCore.Models
{
    public class cat_Predial_TipoUsos
    {
        public int Id { get; set; }
        public int IdPredial { get; set; }
        [Display(Name = "Tipo de Uso")]
        public string TipoUso { get; set; }
        [Display(Name = "Niveles")]
        public string Nivel {  get; set; }
        [Display(Name = "Clase")]
        public string Clase { get; set; }
        [Display(Name = "M2 de Contrucción")]
        public double M2Construccion { get; set; }
        [Display(Name = "Antiguedad")]
        public int Antiguedad { get; set; }
    }
}
