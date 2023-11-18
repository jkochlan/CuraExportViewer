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
using System.ComponentModel;
using System.Globalization;

namespace CuraExportViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public enum CompareEnum
    {
        Unchanged,
        Changed,
        Added,
        Removed
    }

    public partial class MainWindow : Window
    {
        public ObservableCollection<string> CsvFiles { get; set; }
        public ObservableCollection<CsvData> CsvDataList { get; set; }
        public ObservableCollection<CsvData> CsvDataList2 { get; set; }
        public ICollectionView CsvDataView { get; set; }
        public ICollectionView CsvDataView2 { get; set; }

        private List<string> selectedCsvFiles = new List<string>();

        private string folderPath = @"..\..\..\TestData";

        public MainWindow()
        {
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.80);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.80);

            RenameFiles(folderPath);

            InitializeComponent();

            CsvFiles = new ObservableCollection<string>();
            CsvDataList = new ObservableCollection<CsvData>();
            CsvDataList2 = new ObservableCollection<CsvData>();
            CsvDataView = CollectionViewSource.GetDefaultView(CsvDataList);
            CsvDataView2 = CollectionViewSource.GetDefaultView(CsvDataList2);

            // Set the ListBox item source
            csvListBox.ItemsSource = CsvFiles;  

            // Set the DataGrid item source
            csvDataGrid.ItemsSource = CsvDataView;
            csvDataGrid2.ItemsSource = CsvDataView2;

            var fileList = Directory.EnumerateFiles(folderPath, "*.csv")
                                    .OrderByDescending(fileName => ExtractCreationDate(fileName))
                                    .ToList();

            // Populate the ListBox with CSV files
            foreach (string filePath in fileList)
            {
                CsvFiles.Add(Path.GetFileName(filePath));
            }

            // Handle selection changed event
            csvListBox.SelectionChanged += CsvListBox_SelectionChanged;

            // Group the DataGrid by "Section"
            CsvDataView.GroupDescriptions.Add(new PropertyGroupDescription("Section"));
            CsvDataView2.GroupDescriptions.Add(new PropertyGroupDescription("Section"));
        }

        private void CsvListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            csvDataGrid2.Visibility = toolbar.Visibility = csvListBox.SelectedItems.Count == 2 ? Visibility.Visible : Visibility.Collapsed;

            // Limit selection to a maximum of two items
            if (csvListBox.SelectedItems.Count > 2)
            {
                // Deselect the extra items
                foreach (string item in e.RemovedItems)
                {
                    if (!selectedCsvFiles.Remove(item))
                    {
                        // Handle the case where an item was not in the list
                    }
                }

                // Prevent selecting more than two items
                csvListBox.SelectedItems.Clear();
                csvListBox.SelectedItems.Add(selectedCsvFiles.LastOrDefault());
            }
            else
            {
                // Update the list of selected items
                selectedCsvFiles = csvListBox.SelectedItems.Cast<string>().ToList();        
            }

            // Clear previous data
            CsvDataList.Clear();
            CsvDataList2.Clear();

            // Get the selected CSV file

            var parsedData1 = new List<CsvData>();
            if (selectedCsvFiles.Count >= 1)
            {
                parsedData1 = ParseCsv((string)selectedCsvFiles[0]);

                // Read and parse the CSV file
                parsedData1.ForEach(x => CsvDataList.Add(x));
            }

            if (selectedCsvFiles.Count == 2)
            {
                var parsedData2 = ParseCsv((string)selectedCsvFiles[1]);
                // Read and parse the CSV file
                CompareCsvDataCollections(parsedData1, parsedData2);
                parsedData2.ForEach(x => CsvDataList2.Add(x));
            }
        }

        private List<CsvData> ParseCsv(string selectedCsvFile)
        {
            var filePath = Path.Combine(folderPath, selectedCsvFile);   
            var lines = File.ReadAllLines(filePath);
            var csvDataList = new List<CsvData>();


            foreach (string line in lines.Skip(1)) // Skip header
            {
                string[] values = line.Split(';');
                csvDataList.Add(new CsvData
                {
                    Section = values[0],
                    Extruder = values[1],
                    Key = values[2],
                    Type = values[3],
                    Value = values[4]
                });
            }

            return csvDataList;
        }

        private void CompareCsvDataCollections(List<CsvData> collection1, List<CsvData> collection2)
        {
            // Compare the two collections and set ComparisonStatus property
            foreach (var item1 in collection2)
            {
                var matchingItem2 = collection1.FirstOrDefault(item2 => item1.Section == item2.Section && item1.Key == item2.Key);

                if (matchingItem2 == null)
                {
                    // Item in collection1 is not found in collection2
                    item1.ComparisonStatus = CompareEnum.Removed;
                }
                else if (!AreCsvDataEqual(item1, matchingItem2))
                {
                    // Item is present in both collections, but properties are different
                    item1.ComparisonStatus = CompareEnum.Changed;
                    matchingItem2.ComparisonStatus = CompareEnum.Changed;
                }
                else
                {
                    // Item is present in both collections, and properties are the same
                    item1.ComparisonStatus = CompareEnum.Unchanged;
                    matchingItem2.ComparisonStatus = CompareEnum.Unchanged;
                }
            }

            // Find items in collection2 that are not present in collection1
            foreach (var item2 in collection1.Where(item2 => !collection2.Any(item1 => item1.Section == item2.Section && item1.Key == item2.Key)))
            {
                item2.ComparisonStatus = CompareEnum.Added;
            }
        }

        private bool AreCsvDataEqual(CsvData data1, CsvData data2)
        {
            return data1.Extruder == data2.Extruder
                && data1.Key == data2.Key
                && data1.Type == data2.Type
                && data1.Value == data2.Value;
        }

        private void RenameFiles(string directoryPath)
        {
            // Iterate through each item in the ListView
            foreach (var item in Directory.GetFiles(directoryPath))
            {
                // Assuming each item is a file name (string)
                string fileName = item.ToString();

                // Check if the file name starts with an underscore
                if (!Path.GetFileNameWithoutExtension(fileName).StartsWith("_"))
                {
                    // Build the full path to the original file
                    string originalFilePath = fileName;

                    try
                    {
                        // Get the creation date and time of the original file
                        DateTime creationDateTime = File.GetCreationTime(originalFilePath);

                        // Create a new file name with the creation date and time
                        string newFileName = $"_CuraSettings-{creationDateTime:yyyyMMdd_HHmmss}.csv";

                        // Build the full path to the new file
                        string newFilePath = Path.Combine(directoryPath, newFileName);

                        // Rename the file
                        File.Move(originalFilePath, newFilePath);
                    }
                    catch (Exception ex)
                    
                    {
                        // Handle any exceptions that may occur during file renaming
                        MessageBox.Show($"Error renaming file {fileName}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void OrderFilesByCreationDate(ListView listView)
        {
            // Assuming each item in the ListView is a file name (string)
            List<string> fileNames = listView.Items.Cast<string>().ToList();

            // Order the file names based on the embedded creation date
            var orderedFileNames = fileNames
                .OrderBy(fileName => ExtractCreationDate(fileName))
                .ToList();

            // Update the ListView with the ordered file names
            listView.Items.Clear();
            foreach (var fileName in orderedFileNames)
            {
                listView.Items.Add(fileName);
            }
        }

        private DateTime ExtractCreationDate(string fileName)
        {
            // Assuming the date format is "yyyyMMdd_HHmmss"
            string dateString = fileName.Substring(fileName.IndexOf('-') + 1, 15);

            // Parse the date string to a DateTime object
            if (DateTime.TryParseExact(dateString, "yyyyMMdd_HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime creationDate))
            {
                return creationDate;
            }

            // Return DateTime.MinValue if parsing fails
            return DateTime.MinValue;
        }
    }

}