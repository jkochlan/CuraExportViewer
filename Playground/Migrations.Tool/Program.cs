// See https://aka.ms/new-console-template for more information
using Migrations.DAL;
using Migrations.Tool;

//Console.WriteLine("Welcome in this migration parser stuff");

//var parser = new MigrationAnalyzer();
//parser.AnalyzeMigrations();

//DataAPI.AddInterceptor(new ColumnTrackingInterceptor());
//var inter = DataAPI.ReturnDbInterceptors();

//Console.WriteLine("Applying interceptor:");

var allIneedToDo = new AllINeedToDo();
var res = allIneedToDo.GetEntityTypeFromTableName(new DataContext(), "Students");
Console.WriteLine(res);
