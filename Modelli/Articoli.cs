using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrello.Modelli
{
    internal class Articoli : ModelloBase
    {
        public int IdTipoArticolo { get; set; }
        
        public string Descrizione { get; set; }
        public decimal Prezzo { get; set; }
        public virtual TipologiaArticoli TipoArticolo { get; set; } 
    }
}
