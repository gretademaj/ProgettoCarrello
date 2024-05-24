using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrello
{
    internal class Sessione
    {
        public int Id { get; set; }
        public DateTime DataOra { get; set; }    
        public string IndirizzoIp { get; set; }
    }
}
