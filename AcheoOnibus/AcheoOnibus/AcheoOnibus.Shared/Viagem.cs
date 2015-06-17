using System;
using System.Collections.Generic;
using System.Text;

namespace AcheoOnibus
{
    public class Viagem
    {
        public int idViagem { get; set; }
        public int sentidoViagem { get; set; }
        public string destino { get; set; }
        public string origem { get; set; }
        public int idItinerarioFK { get; set; }
    }
}
