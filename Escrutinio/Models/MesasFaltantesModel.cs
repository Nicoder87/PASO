using Escrutinio.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Escrutinio.Models
{
    public class MesasFaltantesModel
    {

        [Display(Name = "CIRCUITO")]
        public string CIRCUITO { get; set; }

        [Display(Name = "ESCUELA")]
        public string ESCUELA { get; set; }
        
        
        [Display(Name = "MESA")]
        public int MESA { get; set; }

        [Display(Name = "CONTACTO")]
        public string CONTACTO { get; set; }


    }


    public class MesasEscrutadasIndexModel
    {
        [Display(Name = "CIRCUITO")]
        public string CIRCUITO { get; set; }

        [Display(Name = "ESCUELA")]
        public string ESCUELA { get; set; }

        [Display(Name = "MESA")]
        public int MESA { get; set; }

        public Guid ORID_MESA { get; set; }

    }


    public class MesasFaltantesIndexModel
    {
        [Display(Name = "CIRCUITO")]
        public string CIRCUITO { get; set; }

        [Display(Name = "ESCUELA")]
        public string ESCUELA { get; set; }


        [Display(Name = "MESA")]
        public int MESA { get; set; }

        [Display(Name = "CONTACTO")]
        public string CONTACTO { get; set; }
    }


}