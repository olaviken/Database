using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArealogUtstyr.ClassStructure.Objects
{
    internal class Utstyr
    {
        public string IDUtstyr { get; set; } = string.Empty;
        public string Romnr { get; set; } = string.Empty; 
        public int IDUnderkategori { get; set; }
        public int IDSLA { get; set; }
        public DateOnly Innkjoepsdato { get; set; }
        public int LevetidAar { get; set; }
        public DateOnly DatoAvskrevet { get; set; }
        public DateOnly DatoSLA { get; set; }
        public DateOnly DatoSistVedlikehold { get; set; }

    }
}
