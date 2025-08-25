using Microsoft.AspNetCore.Http;

namespace WebLomelinCore.Models
{
    public class facturasModel
    {
        public string? rfcEmisor { get; set; }
        public string? rfcReceptor { get; set; }
        public string? folio { get; set; }
        public string? serie { get; set; }
        public string? total { get; set; }
        public string? uuid { get; set; }
        public string? fecha { get; set; }
        public IFormFile xml { get; set; }
        public IFormFile pdf { get; set; }
    }
}