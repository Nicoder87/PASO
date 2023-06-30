using Escrutinio.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Escrutinio.Models
{
    public class PostulacionModel
    {
        public System.Guid ORID { get; set; }
        public System.Guid LISTA { get; set; }
        public System.Guid CARGO { get; set; }
        public int MESA { get; set; }
        public String DescLista { get; set; }
        public String DescCargo { get; set; }
        public int Votos { get; set; }
        public int OrdenCargo { get; set; }
    }

    public class PostulacionEditModel
    {
        public System.Guid ORID { get; set; }
        public System.Guid LISTA { get; set; }
        public System.Guid CARGO { get; set; }

        [Required]
        public String NOMBRE { get; set; }

        [Required]
        public String APELLIDO { get; set; }
    }

    public class PostulacionIndexModel
    {
        public Guid POSTULACION { get; set; }
        public string AGRUPACION { get; set; }
        public string LISTA { get; set; }
        public string CARGO { get; set; }
        public string CANDIDATO { get; set; }
    }


}