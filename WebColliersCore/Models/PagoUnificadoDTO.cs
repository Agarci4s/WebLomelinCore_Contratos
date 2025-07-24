namespace WebLomelinCore.Models
{
    public class PagoUnificadoDTO
    {
        public int Id { get; set; }            // ID del pago
        public int IdCuenta { get; set; }      // ID de la cuenta
        public double Importe { get; set; }    // Monto pagado
        public string TipoServicio { get; set; } // "1"=Agua, "2"=Luz, "3"=Predial
        public int StatusProceso { get; set; }   // 1=Autorizado, 2=Rechazado, etc.

    }
}
