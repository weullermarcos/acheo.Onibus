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
    using System.Runtime.Serialization;
    [DataContract]
    public partial class getOnibus
    {
        [DataMember]
        public string numero { get; set; }
        [DataMember]
        public int sentidoViagem { get; set; }
        [DataMember]
        public string latitude { get; set; }
        [DataMember]
        public string longitude { get; set; }
        [DataMember]
        public double velocidade { get; set; }
        [DataMember]
        public string placa { get; set; }
        [DataMember]
        public int numeroOnibus { get; set; }
        [DataMember]
        public decimal tarifa { get; set; }
        [DataMember]
        public System.DateTime data { get; set; }
    }
}
