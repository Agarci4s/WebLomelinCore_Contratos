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
    }
}
