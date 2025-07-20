using System.ComponentModel.DataAnnotations;
using System;

namespace WebLomelinCore.Models
{
    public class DtPoderes
    {
        public Int64 IdPoderes { get; set; }
        public Int64 IdPropietario { get; set; }

        [Display(Name = "Número de escritura")]
        [MaxLength(20)]
        public string NoEscritura { get; set; } //20

        [Display(Name = "Número de libro")]
        [MaxLength(10)]
        public string NoLibro { get; set; }//10

        [Display(Name = "Volumen")]
        [MaxLength(10)]
        public string Volumen { get; set; } //10

        [Display(Name = "Fecha poder")]
        public DateTime FechaPoder { get; set; }

        [Display(Name = "Nombre del Notario")]
        [MaxLength(50)]
        public string NombreNotario { get; set; }//50

        [Display(Name = "Número de Notario")]
        [MaxLength(10)]
        public string NoNotario { get; set; } //10

        [Display(Name = "Entidad federativa")]
        [MaxLength(40)]
        public string CdNotario { get; set; }//40

        [Display(Name = "Fojas")]
        [MaxLength(10)]
        public string Fojas { get; set; }//10

        [Display(Name = "Nombre del apoderado")]
        [MaxLength(50)]
        public string NombreApoderado { get; set; }//50
    }
}
