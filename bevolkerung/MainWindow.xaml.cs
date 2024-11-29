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
            var methodName = $"Feladat{comboBox.SelectedIndex+1}";
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

        //4.feladat
        //5.feladat
        //6.feladat
        //7.feladat
        //8.feladat
        //9.feladat
        //10.feladat
        //11.feladat
        //12.feladat
        //13.feladat
        //14.feladat
        //15.feladat
        //16.feladat
        //17.feladat
        //18.feladat
        //19.feladat
        //20.feladat


    }
}