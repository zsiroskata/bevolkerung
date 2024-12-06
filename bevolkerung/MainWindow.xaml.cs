using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bevolkerung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Allampolgar> lakossag = new();
        public MainWindow()
        {
            InitializeComponent();

            using StreamReader sr = new(
                path: @"..\..\..\src\bevölkerung.txt",
                encoding: Encoding.UTF8);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                lakossag.Add(new Allampolgar(sr.ReadLine()));
            }
            sr.Close();

            for (int i = 1; i <= 40; i++)
            {
                comboBox.Items.Add($"{i}.");
            }
            adatokDataGrid.ItemsSource = lakossag;

        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            megold.Content = string.Empty;
            var methodName = $"Feladat{comboBox.SelectedIndex + 1}";
            var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance); //Ez a sor megkeresi a metódust az osztályban a megadott methodName névvel. A BindingFlags.NonPublic azt jelzi, hogy a nem publikus metódusokat is keresi, és a BindingFlags.Instance azt jelzi, hogy az aktuális példány metódusait keresi.
            method?.Invoke(this, null); // Feltételezzük, hogy a metódus mindig elérhető Ha a metódus megtalálható, akkor meghívja azt az aktuális példányra (this) null paraméterrel. A ?. operátor biztosítja, hogy csak akkor próbálja meg meghívni a metódust, ha az valóban létezik.
        }

        //1.feladat
        private void Feladat1()
        {
            var n = lakossag.Max(x => x.NettoJovedelem);
            megold.Content = $"legmagasabb nettó: {n}";

        }
        //2.feladat
        private void Feladat2()
        {
            var n = lakossag.Average(x => x.NettoJovedelem);
            megold.Content = $"átlagos nettó jövedelem: {n}";
        }
        //3.feladat
        private void Feladat3()
        {
            listBox.Items.Clear();
            var n = lakossag.GroupBy(x => x.Tartomany).Select(x => new { Tartomany = x.Key, Count = x.Count() });

            foreach (var item in n)
            {
                listBox.Items.Add($"{item.Tartomany}: {item.Count} állampolgár");
            }
        }

        //4.feladat
        private void Feladat4()
        {
            listBox.Items.Clear();
            var n = lakossag.Where(x => x.Nemzetiseg == "angolai");
            foreach (var item in n)
            {
                listBox.Items.Add(item.ToString(false));
            }
        }

        //5.feladat
        private void Feladat5()
        {
            listBox.Items.Clear();
            var min = lakossag.Min(x => x.SzuletesiEv);
            var n = lakossag.Where(x => x.SzuletesiEv == min);

            foreach (var item in n)
            {
                listBox.Items.Add(item.ToString(false));
            }
        }

        //6.feladat
        private void Feladat6()
        {
            listBox.Items.Clear();
            var nincsCigi = lakossag.Where(x => !x.Dohanyzik);

            foreach (var item in nincsCigi)
            {
                listBox.Items.Add($"ID: {item.Id}, Havi jövedelem: {item.NettoJovedelem / 12:F2} EUR");
            }
        }

        //7.feladat
        private void Feladat7()
        {
            listBox.Items.Clear();
            var n = lakossag.Where(x => x.Tartomany == "Bajorország" && x.NettoJovedelem > 30000).OrderBy(x => x.IskolaiVegzettseg);

            foreach (var item in n)
            {
                listBox.Items.Add(item.ToString(false));
            }
        }

        //8.feladat
        private void Feladat8()
        {
            listBox.Items.Clear();
            var fiuk = lakossag.Where(a => a.Nem == "férfi");
            foreach (var item in fiuk)
            {
                listBox.Items.Add(item.ToString(true));
            }
        }
        //9.feladat
        private void Feladat9()
        {
            listBox.Items.Clear();
            var n = lakossag.Where(x => x.Tartomany == "Bajorország" && x.Nem == "nő");
            foreach (var item in n)
            {
                listBox.Items.Add(item.ToString(false));
            }
        }

        //10.feladat
        private void Feladat10()
        {
            listBox.Items.Clear();
            var noSmoke = lakossag.Where(x => !x.Dohanyzik).OrderByDescending(x => x.NettoJovedelem).Take(10);
            foreach (var item in noSmoke)
            {
                listBox.Items.Add(item.ToString(true));
            }
        }


        //11.feladat
        private void Feladat11()
        {
            listBox.Items.Clear();
            //Adja vissza az 5 legidősebb állampolgár összes adatát.
            var n = lakossag.OrderBy(x => x.SzuletesiEv).Take(5).ToString();
            foreach (var item in n)
            {
                listBox.Items.Add(item);
            }

        }

        //12.feladat
        private void Feladat12()
        {
            listBox.Items.Clear();
            var csoport = lakossag.Where(a => a.Nemzetiseg == "német").GroupBy(a => a.Nepcsoport);

            foreach (var item in csoport)
            {
                listBox.Items.Add($"{item.Key}:");
                foreach (var x in item)
                {
                    string szavazo = x.AktivSzavazo ? "aktív szavazó" : "nem aktív szavazó";
                    listBox.Items.Add($"  {szavazo}, Politikai nézet: {x.PolitikaikaiNezet}");
                }
            }
        }
        //13.feladat
        private void Feladat13()
        {
            var sorfogyasztas = lakossag.Where(x => x.Nem == "férfi").Average(x => x.SorFogyasztasEvente);
            megold.Content = $"Az éves átlagos sörfogyasztás a férfiak körében: {sorfogyasztas:F2} liter.";
        }


        //14.feladat
        private void Feladat14()
        {
            listBox.Items.Clear();
            var groupedByEducation = lakossag.OrderBy(x => x.IskolaiVegzettseg).GroupBy(x => x.IskolaiVegzettseg);

            foreach (var group in groupedByEducation)
            {
                listBox.Items.Add($"{group.Key}:");
                foreach (var person in group)
                {
                    listBox.Items.Add($"  {person}");
                }
            }
        }

        //15.feladat
        private void Feladat15()
        {
            listBox.Items.Clear();
            var harom = lakossag.OrderByDescending(x => x.NettoJovedelem).Take(3);
            var top3LowIncome = lakossag.OrderBy(x => x.NettoJovedelem).Take(3);

            listBox.Items.Add("Legmagasabb jövedelmek:");
            foreach (var item in harom)
            {
                listBox.Items.Add(item.ToString(false));
            }

            listBox.Items.Add("Legalacsonyabb jövedelmek:");
            foreach (var item in top3LowIncome)
            {
                listBox.Items.Add(item.ToString(false));
            }
        }
        //16.feladat
        private void Feladat16()
        {
            int osszes = lakossag.Count;
            int aktiv = lakossag.Count(x => x.AktivSzavazo);

            double percentage = (double)aktiv / osszes * 100;
            megold.Content = $"Az aktív szavazók aránya: {percentage:F2}%.";
        }
        //17.feladat
        private void Feladat17()
        {
            listBox.Items.Clear();
            var csoport = lakossag.Where(x => x.AktivSzavazo).OrderBy(x => x.Tartomany).GroupBy(x => x.Tartomany);
            foreach (var item in csoport)
            {
                listBox.Items.Add($"{item.Key}:");
                foreach (var c in item)
                {
                    listBox.Items.Add($"  {c}");
                }
            }
        }
        //18.feladat
        private void Feladat18()
        {
            int most = DateTime.Now.Year;
            double atlagKor = lakossag.Average(a => most - a.SzuletesiEv);

            megold.Content = $"Az átlagos életkor: {atlagKor:F2} év.";
        }
        //19.feladat
        private void Feladat19()
        {
            var hivok = lakossag.GroupBy(x => x.Tartomany)
                .Select(x => new
                {
                    Region = x.Key,
                    AvgIncome = x.Average(x => x.NettoJovedelem),
                    Population = x.Count()
                });

            var legmagasabb = hivok.OrderByDescending(x => x.AvgIncome).ThenByDescending(x => x.Population).First();

            megold.Content = $"A legmagasabb átlagjövedelem tartománya: {legmagasabb.Region}, " +
                                       $"Átlagos jövedelem: {legmagasabb.AvgIncome:F2} EUR, " +
                                       $"Lakosság száma: {legmagasabb.Population}.";
        }
        //20.feladat
        private void Feladat20()
        {
            double atlagsuly = lakossag.Average(a => a.Suly);

            var medium = lakossag.Select(x => x.Suly).OrderBy(x => x).ToList();

            double medianWeight = medium.Count % 2 == 0 ? (medium[medium.Count / 2 - 1] + medium[medium.Count / 2]) / 2.0 : medium[medium.Count / 2];

            megold.Content = $"Az átlagos súly: {atlagsuly:F2} kg, Medián súly: {medianWeight:F2} kg.";
        }





        //21.feladat
        private void Feladat21()
        {
            var szavazokSor = lakossag.Where(x => x.AktivSzavazo && x.SorFogyasztasEvente != -1).Average(x => x.SorFogyasztasEvente);

            var nemSzavazok = lakossag.Where(a => !a.AktivSzavazo && a.SorFogyasztasEvente != -1).Average(a => a.SorFogyasztasEvente);

            string decision = szavazokSor > nemSzavazok ? "Az aktív szavazók fogyasztanak több sört." : "A nem szavazók fogyasztanak több sört.";

            listBox.Items.Add($"Aktív szavazók átlagos sörfogyasztása: {szavazokSor:F2} liter.");
            listBox.Items.Add($"Nem szavazók átlagos sörfogyasztása: {nemSzavazok:F2} liter.");
            megold.Content = decision;
        }

        //22.feladat
        private void Feladat22()
        {
            var ferfiakAtlagMagassag = lakossag
                .Where(a => a.Nem == "férfi")
                .Average(a => a.Magassag);

            var nokAtlagMagassag = lakossag.Where(a => a.Nem == "nő").Average(a => a.Magassag);

            megold.Content = $"Ferfiak átlag magassága : {ferfiakAtlagMagassag:F1}cm, nők átlag magassága: {nokAtlagMagassag:F1}cm";

        }

        //23.feladat
        private void Feladat23()
        {
            var csopi = lakossag.GroupBy(x => x.Nepcsoport)
                .Select(x => new
                {
                    Ethnicity = x.Key,
                    Count = x.Count(),
                    AvgAge = x.Average(a => DateTime.Now.Year - a.SzuletesiEv)
                })
                .OrderByDescending(x => x.Count)
                .ThenByDescending(x => x.AvgAge)
                .First();

            megold.Content = $"A legnépesebb népcsoport: {csopi.Ethnicity}, " +
                                       $"Létszám: {csopi.Count}, Átlagos életkor: {csopi.AvgAge:F2} év.";
        }

        //24.feladat
        private void Feladat24()
        {
            var cigiAtlag = lakossag.Where(x => x.Dohanyzik).Average(x => x.NettoJovedelem);

            var nincsCigiAtlag = lakossag.Where(x => !x.Dohanyzik).Average(x => x.NettoJovedelem);

            megold.Content = $"Dohányzók átlagos nettó éves jövedelme: {cigiAtlag:F2} EUR, " +
                                       $"Nem dohányzók: {nincsCigiAtlag:F2} EUR.";
        }

        //25.feladat
        private void Feladat25()
        {
            var krumpli = lakossag.Where(x => x.KrumpiFogyasztasEvente != -1).Average(x => x.KrumpiFogyasztasEvente);

            listBox.Items.Add($"Átlagos krumplifogyasztás: {krumpli:F2} kg évente.");
            var n = lakossag.Where(x => x.KrumpiFogyasztasEvente > krumpli).Take(15);
            foreach (var item in n)
            {
                listBox.Items.Add(item.ToString(false));
            }
        }

        //26.feladat
        private void Feladat26()
        {
            listBox.Items.Clear();
            var most = DateTime.Now.Year;
            var n = lakossag.GroupBy(x => x.Tartomany)
                .Select(x => new
            {
                tartomany = x.Key,
                atlagKor = x.Average(a => most - a.SzuletesiEv)
            });

            foreach (var item in n)
            {
                listBox.Items.Add($"Tartomány: {item.tartomany}\tátlag életkor: {item.atlagKor:F2}");
            }
        }

        //27.feladat
        private void Feladat27()
        {
            listBox.Items.Clear();
            var most = DateTime.Now.Year;

            // Szűrés: csak az 50 évnél idősebbek
            var n = lakossag.Where(x => x.Eletkor(most) > 50).ToList();

            foreach (var item in n.Take(5))
            {
                listBox.Items.Add(item); 
            }
            listBox.Items.Add($"Összesen {n.Count} személy van, aki 50 évnél idősebb.");
        }

        //28.feladat

    }
}