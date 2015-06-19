using System;
using System.Collections.Generic;
using System.Text;

namespace AcheoOnibus
{
    class Onibus
    {
        public string numero { get; set; }
        public string origem { get; set; }
        public string destino { get; set; }
        public int sentidoViagem { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string placa { get; set; }
        public int numeroOnibus { get; set; }
        public decimal tarifa { get; set; }
        public System.DateTime data { get; set; }
    }
}
