namespace CsvReader.Ingress;

public class RequestFilePathContext : IContext
{
    public IContext Transition()
    {
        string filePath = GetFilePathFromUser();
        
        if (File.Exists(filePath))
            return new FileProcessingContext(filePath);
        
        ReportFileNotFound(filePath);
        return this;
    }
    
    private string GetFilePathFromUser()
    {
        string? filePath;
        do
        {
            Console.WriteLine(IngressMessages.RequestFilePath);
            filePath = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(filePath));

        return filePath;
    }

    private void ReportFileNotFound(string filePath)
    {
        Console.WriteLine(IngressMessages.FileNotFound(filePath));
        Console.WriteLine();
    }
}