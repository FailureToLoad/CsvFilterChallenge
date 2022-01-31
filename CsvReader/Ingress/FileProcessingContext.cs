namespace CsvReader.Ingress;

public class FileProcessingContext : IContext
{
    private readonly string _filePath;
    public FileProcessingContext(string filePath)
    {
        _filePath = filePath;
    }
    public IContext Transition()
    {
        using StreamReader reader = new StreamReader(_filePath);
        if(reader.Peek() <= 0)
            return new ErrorReportingContext(IngressMessages.HeadersMissing());
        
        var headerMap = BuildHeaderMap(reader.ReadLine()!.Split(","));
        return new DocumentReadyContext();
    }
    
    private Dictionary<string,int> BuildHeaderMap(string[] headers)
    {
        Dictionary<string, int> dict = new Dictionary<string, int>();
        for (int index = 0; index < headers.Length; index++)
            dict.Add(headers[index],index);
        return dict;
    }
}