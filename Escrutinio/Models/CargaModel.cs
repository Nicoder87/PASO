using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Escrutinio.Models
{
    public class CargaModel
    {
        [Display(Name = "MESA (*)")]
        public int Mesa { get; set; }

        [Display(Name = "VOTOS EMITIDOS (*)")]
        public int Votos_emitidos { get; set; }

        public String[] Postulacion { get; set; }
        public int[] Votos { get; set; }

        public int[] Nulos { get; set; }
        public int[] Recurridos { get; set; }
        public int[] Impugnados { get; set; }
        public int[] Blancos { get; set; }

        public int[] Validos { get; set; }
        public int[] Totales { get; set; }

        public MesaModel MesaModel { get; set; }
        public DateTime InicioCarga { get; set; }
        public List<PostulacionModel> ListaPostulacion { get; set; }

        public ValidosTotalesModel ValidosTotales { get; set; }

        public List<TotalesOtrosVotosModel> otros_votos { get; set; }

        public Guid ORID_MESA { get; set; }
    }
}