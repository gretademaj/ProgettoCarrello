using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrello
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new Db();
            if (db.HaSessione(Environment.MachineName))
            {
                Console.WriteLine("L'utente ha una sessione attiva.");
                Console.WriteLine("Ecco la lista degli articoli da aggiungere nel carrello \n");

                var returndati = db.GetArticoli();
                foreach (var item in returndati)
                {
                    var stampaDati = string.Empty;
                    stampaDati += $"L'articolo con il codice {item.Id} ";
                    stampaDati += $"e Descrizione {item.Descrizione}";
                    stampaDati += $" costa {item.Prezzo}\n";

                    Console.WriteLine(stampaDati);
                }
                Console.WriteLine("Scegli l'articolo:");
            }
            else
            {
                Console.WriteLine("Creazione sessione!");
                db.CreaSessione(Environment.MachineName);

            }

            var p = Console.ReadKey();
            var sessione = db.GetSessione(Environment.MachineName);
            switch (p.KeyChar)
            {


                case '1':
                    db.InserisciArticolo(sessione.Id, 1);
                    Console.WriteLine("Per confermare l'ordine premi il tasto x");
                    break;
                case '2':
                    db.InserisciArticolo(sessione.Id, 2);
                    Console.WriteLine("Per confermare l'ordine premi il tasto x");
                    break;
                case '3':
                    db.InserisciArticolo(sessione.Id, 3);
                    Console.WriteLine("Per confermare l'ordine premi il tasto x");
                    break;
                default:
                    break;
            }
            var inputOrdine = Console.ReadKey();
            if(inputOrdine.KeyChar=='x')
            {
                var carrello = db.GetCarrello(sessione.Id);
                decimal totale =0;
                foreach (var articolo in carrello.Articoli)
                {
                    totale += articolo.Prezzo;
                }

                db.ConfermaOrdine(sessione.Id, totale);
                Console.WriteLine("Ordine confermato! Totale da pagare : " + totale);

            }
            Console.ReadLine();



        }
    }
}
