using System;
using System.ComponentModel.DataAnnotations;
using Escrutinio.Database;
using System.Web.Mvc;

namespace Escrutinio.Models
{
    public class GraficosModel
    {
        public int data { get; set; }
        public int value { get; set; }
        public string label { get; set; }

        [Required(ErrorMessage = "Debes ingresar Fecha Desde")]
        [Display(Name = "Desde")]
        public Nullable<DateTime> FechaD { get; set; }

        [Required(ErrorMessage = "Debes ingresar Fecha Hasta")]
        [Display(Name = "Hasta")]
        public Nullable<DateTime> FechaH { get; set; }

        [Required(ErrorMessage = "Debes ingresar el Año")]
        [StringLength(4, ErrorMessage = "El campo {0} debe tener {1} caracteres", MinimumLength = 4)]
        public string AÑO { get; set; }

    }
}