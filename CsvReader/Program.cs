// See https://aka.ms/new-console-template for more information

using CsvReader.Ingress;
using CsvReader.Query;

IngressStrategy ingressStrat = new IngressStrategy();
var document = ingressStrat.ReadCsvFromFile();

QueryStrategy queryStrat = new();
var results = queryStrat.QueryDocument(document);

Console.WriteLine("Results:");
foreach(var result in results)
    Console.WriteLine(string.Join(" ",result));