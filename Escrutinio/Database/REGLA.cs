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
    
    public partial class REGLA
    {
        public REGLA()
        {
            this.ROL = new HashSet<ROL>();
        }
    
        public System.Guid ORID { get; set; }
        public string DEFINICION { get; set; }
        public string DESCRIPCION { get; set; }
    
        public virtual ICollection<ROL> ROL { get; set; }
    }
}
