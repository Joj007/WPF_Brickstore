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
using Microsoft.Win32;

namespace Bricks
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Termek> termekek = new();
        string fileName;
        public MainWindow()
        {
            InitializeComponent();
            dgTermekLista.ItemsSource = termekek;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "bsx files (*.bsx)|*.bsx|All files (*.*)|*.*";
            openFileDialog1.ShowDialog();
            fileName = openFileDialog1.FileName;
            Betolt(fileName);
            
        }

        private void Betolt(string fileName)
        {
            List<string> sorok = File.ReadAllLines(fileName).ToList();

            for (int i = 0; i < sorok.Count; i++)
            {
                if (sorok[i].Trim() == "<Item>")
                {
                    List<string> termekAdatok = new();
                    for (int j = 1; j < 13; j++)
                    {
                        int tol = sorok[i + j].IndexOf(">");
                        int ig = sorok[i + j].LastIndexOf("<");
                        int length = sorok[i + j].Length;

                        termekAdatok.Add(sorok[i + j].Substring(tol + 1, ig - tol - 1));

                    }
                    Termek ujTermerk = new Termek(termekAdatok.ToArray());

                    if (txtSzuro.Text!="")
                    {
                        if(ujTermerk.Name.StartsWith(txtSzuro.Text)|| ujTermerk.Id.StartsWith(txtSzuro.Text))
                        {
                            termekek.Add(ujTermerk);
                        }
                    }
                    else
                    {
                        termekek.Add(ujTermerk);
                    }

                    

                }
            }
        }

        private void btnSzuro_Click(object sender, RoutedEventArgs e)
        {
            termekek = new();
            dgTermekLista.ItemsSource = termekek;
            Betolt(fileName);
        }
    }
}