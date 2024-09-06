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
using System.IO;
using System.Collections.ObjectModel;

namespace Bricks
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Termek> termekek = new ObservableCollection<Termek>();
        public MainWindow()
        {
            InitializeComponent();
            dgTermekLista.ItemsSource = termekek;
            List<string> sorok = File.ReadAllLines("brickstore_parts_7288-1-mobile-police-unit.bsx").ToList();

            for (int i = 0; i < sorok.Count; i++)
            {
                if (sorok[i].Trim()=="<Item>")
                {
                    List<string> termekAdatok = new();
                    for (int j = 1; j < 13; j++)
                    {
                        int tol = sorok[i + j].IndexOf(">");
                        int ig = sorok[i + j].LastIndexOf("<");
                        int length = sorok[i + j].Length;

                        termekAdatok.Add(sorok[i + j].Substring(tol+1, ig-tol-1));
                        
                    }
                    termekek.Add(new Termek(termekAdatok.ToArray()));

                }
            }


        }
    }
}