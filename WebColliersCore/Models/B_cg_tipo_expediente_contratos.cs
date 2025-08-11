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
    public class B_cg_tipo_expediente_contratos
    {
        public int id_b_cg_tipo_expediente_contratos { get; set; }

        public string categoria { get; set; }

        public string sub_categoria { get; set; }

        public bool status { get; set; }


        //crear un arreglo del detalle

    }

}

