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
    
    public partial class CIRCUITO
    {
        public CIRCUITO()
        {
            this.ESCUELA = new HashSet<ESCUELA>();
        }
    
        public System.Guid ORID { get; set; }
        public string CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<int> PRIORIDAD { get; set; }
    
        public virtual ICollection<ESCUELA> ESCUELA { get; set; }
    }
}
