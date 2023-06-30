using Escrutinio.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Escrutinio.Models
{
    public class CircuitosEscrutadosModel
    {

        [Display(Name = "CIRCUITO")]
        public string CIRCUITO { get; set; }

        [Display(Name = "PROCENTAJE")]
        public string PORCENTAJE { get; set; }
    }



    public class CircuitosEscrutadosIndexModel
    {
        [Display(Name = "CIRCUITO")]
        public string CIRCUITO { get; set; }

        [Display(Name = "PROCENTAJE")]
        public string PORCENTAJE { get; set; }

    }


}