using System;
using System.Collections.Generic;

namespace Escrutinio.Models
{
    public class TodosModel
    {
        public List<TotalesReferenciaModel> REFERENCIA { get; set; }
        public VotosModel VOTOS { get; set; }

        public Guid IdCircuito { get; set; }
    }

    public class VotosModel
    {
        public List<ListasCompletasModel> LISTAS_COMPLETAS { get; set; }
        public List<TotalesOtrosVotosModel> OTROS_VOTOS { get; set; }
        public Nullable<bool> TIPO { get; set; }
    }

    public class TotalesReferenciaModel
    {
        public int MESAS_HABILITADAS { get; set; }
        public int VOTANTES_HABILITADOS { get; set; }
        public int MESAS_ESCRUTADAS { get; set; }
        public decimal MESAS_ESCRUTADAS_PERCENT { get; set; }
        public int VOTANTES_ESCRUTADOS { get; set; }
        public decimal VOTANTES_ESCRUTADOS_PERCENT { get; set; }
    }

    public class TotalesOtrosVotosModel
    {
        public string DESCRIPCION { get; set; }
        public int VALIDOS { get; set; }
        public decimal VALIDOS_PERCENT { get; set; }
        public int BLANCOS { get; set; }
        public decimal BLANCOS_PERCENT { get; set; }
        public int NULOS { get; set; }
        public decimal NULOS_PERCENT { get; set; }
        public int RECURRIDOS { get; set; }
        public decimal RECURRIDOS_PERCENT { get; set; }
        public int IMPUGNADOS { get; set; }
        public decimal IMPUGNADOS_PERCENT { get; set; }
        public int TOTALES { get; set; }
    }

    public class ListasCompletasModel
    {
        public string DESCRIPCION_CORTA { get; set; }
        public string LISTA_CORTA { get; set; }
        public string LISTA { get; set; }
        public string Code { get; set; }
        public string PRESIDENTE { get; set; }
        public string DIP_NAC { get; set; }
        public string GOBERNADOR { get; set; }
        public string LEG_PROV { get; set; }
        public string INTENDENTE { get; set; }
    }
}
