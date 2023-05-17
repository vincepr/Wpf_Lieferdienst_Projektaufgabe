using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Lieferdienst
{
    // the wrapper for our Data coming as JSON form the DB/PHP pipeline
    public class Food
    {
        public int eid { get; set; }
        public string bezeichnung { get; set; }
        public double preis { get; set; }
        public string info { get; set; }
        public string bild { get; set; }
        public string GetPreis => preis.ToString() + " €";

        public override string ToString()
        {
            string bez = bezeichnung.ToString();
            if (bez.Length > 7) return $"{eid} \t| {bezeichnung}\t| {preis} \t| {info}";
            return $"{eid} \t| {bezeichnung} \t\t| {preis} \t| {info}";
        }
    }

    // wrapper für abgeschlossene Bestellvorgänge (Historie aller...)
    public class Bestellung
    {
        public int eid { get; set; }
        public string bezeichnung { get; set; }
        public double preis { get; set; }
        public string bild { get; set; }
        public string datum { get; set; }
        public uint anzahl { get; set; }
        public int bid { get; set; }
        public string GetPreis => preis.ToString() + " €";
        public string GetMenge => anzahl.ToString() + " Stück";
        public string GetSumme => (preis*anzahl).ToString() + " €";
    }
}
