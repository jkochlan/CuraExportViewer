// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

//Console.WriteLine("Hello, World!");


// create comparison test to compare performance for adding unique items to list and hashset
// create a list of 100,000 unique items    

var sourceList = new List<int>();
var destList1 = new List<int>();
var destList2 = new List<int>();

Console.WriteLine("Creating source list with 100,000 unique items");
for (int i = 0; i < 1000000; i++)
{
    sourceList.Add(i);
}

var listStopwatch1 = new Stopwatch();
var listStopwatch2 = new Stopwatch();

Console.WriteLine("Adding items to destination list without hashset");
listStopwatch1.Start();
foreach (var item in sourceList)
{
    if (!destList1.Contains(item))
    {
        destList1.Add(item);
    }
}
listStopwatch1.Stop();

Console.WriteLine($"Time to add items without hashset: {listStopwatch1.ElapsedMilliseconds} ms");


Console.WriteLine("Adding items to destination list with hashset");
listStopwatch2.Start();
var hashSet = new HashSet<int>();
foreach (var item in sourceList)
{
    if (hashSet.Add(item))
    {
        destList2.Add(item);
    }
}
listStopwatch2.Stop();

Console.WriteLine($"Time to add items with hashset: {listStopwatch2.ElapsedMilliseconds} ms");

