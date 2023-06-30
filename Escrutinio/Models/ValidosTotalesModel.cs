using Escrutinio.Database;
using System.Collections.Generic;

namespace Escrutinio.Models
{
    public class ValidosTotalesModel
    {
        public MESA VtMesa { get; set; }

        public List<OtrosVotosModel> VtOtrosVotos { get; set; }

        public List<CARGO> VtCargos { get; set; }

    }
}