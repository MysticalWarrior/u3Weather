using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace u3weather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.OpenRead("https://weather.gc.ca/city/pages/on-88_metric_e.html");
            System.IO.StreamReader streamReader = new System.IO.StreamReader
                (webClient.OpenRead("https://weather.gc.ca/city/pages/on-88_metric_e.html"));
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("Text.txt");
            bool writeNextLine = false;
            try
            {
                string temp = "";
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                   
                    if (writeNextLine == true)
                    {
                        temp = line.Substring(line.IndexOf("\x003E") + 1, line.IndexOf('\x0026') - line.IndexOf("\x003E") - 1);
                        MessageBox.Show(line);
                        MessageBox.Show(temp);
                        streamWriter.WriteLine(line);
                        streamWriter.WriteLine(temp);
                    }
                    writeNextLine = line.Contains("Temperature");
                }
                streamWriter.Flush();
                streamWriter.Close();
                streamReader.Close();
                //MessageBox.Show("read everything");
                Label myLabel = new Label();
                myLabel.Content = "temperature is: " + temp;
                myGrid.Children.Add(myLabel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
