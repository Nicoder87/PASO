//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Escrutinio.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class CARGO
    {
        public CARGO()
        {
            this.POSTULACION = new HashSet<POSTULACION>();
            this.ESCRUTINIO_MESA = new HashSet<ESCRUTINIO_MESA>();
        }
    
        public System.Guid ORID { get; set; }
        public string CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
        public bool APLICA_EXT { get; set; }
        public Nullable<int> ORDEN { get; set; }
        public string DESCRIPCION_CORTA { get; set; }
    
        public virtual ICollection<POSTULACION> POSTULACION { get; set; }
        public virtual ICollection<ESCRUTINIO_MESA> ESCRUTINIO_MESA { get; set; }
    }
}
