using System.ComponentModel.DataAnnotations;

namespace Escrutinio.Models
{
    public class MesaModel
    {
        [Display(Name = "CANTIDAD VOTANTES")]
        public int CantVotantes { get; set; }

        [Display(Name = "ESCUELA")]
        public string Escuela { get; set; }

        [Display(Name = "CIRCUITO")]
        public string Circuito { get; set; }

        [Display(Name = "TIPO")]
        public string Tipo { get; set; }
    }
}