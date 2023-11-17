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
using System.Collections.ObjectModel;
using System.IO;
using Path = System.IO.Path;

namespace CuraExportViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : Window
    {
        public ObservableCollection<string> CsvFiles { get; set; }
        public ObservableCollection<CsvData> CsvDataList { get; set; }

        private string folderPath = @"D:\Private\3Dprint";

        public MainWindow()
        {
            InitializeComponent();

            CsvFiles = new ObservableCollection<string>();
            CsvDataList = new ObservableCollection<CsvData>();

            // Set the ListBox item source
            csvListBox.ItemsSource = CsvFiles;

            // Set the DataGrid item source
            csvDataGrid.ItemsSource = CsvDataList;

            // Specify the folder path where your CSV files are located
            

            // Populate the ListBox with CSV files
            foreach (string filePath in Directory.EnumerateFiles(folderPath, "*.csv"))
            {
                CsvFiles.Add(Path.GetFileName(filePath));
            }

            // Handle selection changed event
            csvListBox.SelectionChanged += CsvListBox_SelectionChanged;
        }

        private void CsvListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Clear previous data
            CsvDataList.Clear();

            // Get the selected CSV file
            string selectedCsvFile = csvListBox.SelectedItem as string;

            if (selectedCsvFile != null)
            {
                // Read and parse the CSV file
                string filePath = Path.Combine(folderPath, selectedCsvFile);
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines.Skip(1)) // Skip header
                {
                    string[] values = line.Split(';');
                    CsvDataList.Add(new CsvData
                    {
                        Section = values[0],
                        Extruder = values[1],
                        Key = values[2],
                        Type = values[3],
                        Value = values[4]
                    });
                }
            }
        }
    }

}