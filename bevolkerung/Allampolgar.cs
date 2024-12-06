using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace bevolkerung
{
    internal class Allampolgar
    {
        private bool dohanyzik;
        private bool szavazo;

        public string Dohanyzo => Dohanyzik ? "igen" : "nem";
        public string Szavazo  => AktivSzavazo ? "igen" : "nem";

        public int Id { get; set; }
        public string Nem { get; set; }
        public int SzuletesiEv { get; set; }
        public int Suly { get; set; }
        public int Magassag { get; set; }
        public bool Dohanyzik { get; set; }
        public string Nemzetiseg { get; set; }
        public string Nepcsoport { get; set; }
        public string Tartomany { get; set; }
        public int NettoJovedelem { get; set; }
        public string IskolaiVegzettseg { get; set; }
        public string PolitikaikaiNezet { get; set; }
        public bool AktivSzavazo { get; set; }
        public int? SorFogyasztasEvente { get; set; }
        public int? KrumpiFogyasztasEvente { get; set; }


        public Allampolgar(string sor)
        {
            var s = sor.Split(";");
            Id = int.Parse(s[0]);
            Nem = s[1];
            SzuletesiEv = int.Parse(s[2]);
            Suly = int.Parse(s[3]);
            Magassag = int.Parse(s[4]);
            Dohanyzik = s[5] == "igen";     //else kell
            Nemzetiseg = s[6];
            Nepcsoport = string.IsNullOrEmpty(s[7]) ? "német" : s[7];       //no bueno
            Tartomany = s[8];
            NettoJovedelem = int.Parse(s[9]);
            IskolaiVegzettseg = s[10];
            PolitikaikaiNezet = s[11];
            AktivSzavazo = s[12] == "igen";
            SorFogyasztasEvente = s[13] == "NA" ? (int?)null : int.Parse(s[13]);
            KrumpiFogyasztasEvente = s[14] == "NA" ? (int?)null : int.Parse(s[14]);
        }
        public int HaviNetto()
        {
            return NettoJovedelem / 12;
        }
        public int Eletkor(int aktualisEv)
        {
            return aktualisEv - SzuletesiEv;
        }
        public string ToString(bool eredmeny)
        {
            if (eredmeny)
            {
                return $"{Id}\t{Nem}\t{SzuletesiEv}\t{Suly}\t{Magassag}";
            }
            else
            {
                return $"{Id}\t{Nemzetiseg}\t{Nepcsoport}\t{Tartomany}\t{NettoJovedelem}";
            }
        }

    }
}
