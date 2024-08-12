// See https://aka.ms/new-console-template for more information
using Migrations.Tool;

Console.WriteLine("Welcome in this migration parser stuff");

var parser = new MigrationAnalyzer();
parser.AnalyzeMigrations();
