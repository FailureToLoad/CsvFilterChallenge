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
        throw new NotImplementedException();
    }
}