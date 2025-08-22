using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using NPOI.SS.Formula.Functions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using WebLomelinCore.Models;

namespace WebColliersCore.Models
{
    public class B_inmuebles_expediente
    {
        public List<B_cg_tipo_expediente_contratos> b_cg_tipo_expediente_contratos { get; set; }

        public B_inmuebles b_inmuebles { get; set; }


    }

}

