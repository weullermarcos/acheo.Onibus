//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace acheoOnibusAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOnibusItinerario
    {
        public int idOnibusItinerario { get; set; }
        public System.DateTime data { get; set; }
        public int idOnibusFK { get; set; }
        public int idItinerarioFK { get; set; }
    
        public virtual tblItinerario tblItinerario { get; set; }
        public virtual tblOnibus tblOnibus { get; set; }
    }
}