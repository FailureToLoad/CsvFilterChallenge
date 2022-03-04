namespace CsvReader.Ingress;

public static class IngressMessages
{
    public static string RequestFilePath => "Please enter the filepath of the CSV file for processing.";
    public static string FileNotFound(string filePath) => $"File not found at {filePath}.";
    public static string HeadersMissing() => "No headers found";
    public static string DataMismatch() => "Found unequal amount of row and header elements.";
    public static string NoValueRows() => "No value rows found.";
    public static string DocumentReady => "Document ready for querying.";
}