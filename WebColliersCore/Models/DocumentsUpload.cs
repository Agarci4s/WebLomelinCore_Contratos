using Microsoft.AspNetCore.Http;

namespace WebLomelinCore.Models
{
    public class DocumentsUpload
    {
        public IFormFile Archivo { get; set; }
        public int? IdRegistro { get; set; }
        public string RutaArchivo { get; set; }
    }
}