using System;

namespace Escrutinio.Models
{
    public class ResultadosModel
    {
    }

    public class CargoListaModel
    {
        public string LISTA { get; set; }
        public string CANDIDATO { get; set; }
        public string COLOR { get; set; }
        public int VOTOS { get; set; }
        public int PORCENTAJE_E { get; set; }
        public string PORCENTAJE_D { get; set; }

        public Guid IdCargo { get; set; }
    }

    public class CargoCircuitoModel
    {
        public string CIRCUITO { get; set; }
        public string LISTA { get; set; }
        public int NUMERO { get; set; }
        public string PORCENTAJE_D { get; set; }
        public string COLOR { get; set; }

        public Guid IdCargo { get; set; }
    }
}
