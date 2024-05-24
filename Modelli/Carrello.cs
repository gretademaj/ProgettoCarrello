using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrello.Modelli
{
    internal class Carrello :ModelloBase
    {
        public int IdSessione { get; set; }
        public int IdArticolo { get; set;}
        public virtual List<Articoli> Articoli { get; set; }
        public virtual Sessione Sessione { get; set; }


    }
}
