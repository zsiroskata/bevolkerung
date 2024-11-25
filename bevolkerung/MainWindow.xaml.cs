using System.IO;
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
       
    }
}