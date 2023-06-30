using System.ComponentModel.DataAnnotations;

namespace Escrutinio.Models
{
    public class ConcejalesModel
    {
        [Display(Name = "LISTA")]
        public string Lista { get; set; }

        [Display(Name = "VOTOS")]
        public int Votos { get; set; }

        [Display(Name = "COCIENTE")]
        public decimal Cociente { get; set; }

        [Display(Name = "TOTAL")]
        public int Total { get; set; }

        [Display(Name = "ENTERO")]
        public int Enteros { get; set; }

        [Display(Name = "RESTO ASIGNADO")]
        public int RestoAsignado { get; set; }

        [Display(Name = "SOBRA ASIGNADA")]
        public int SobraAsignada { get; set; }

    }
}
