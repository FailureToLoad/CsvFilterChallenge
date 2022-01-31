namespace CsvReader.Ingress;

public class ErrorReportingContext : IContext
{
    private readonly string _message;
    public ErrorReportingContext(string message)
    {
        _message = message;
    }
    public IContext Transition()
    {
        Console.WriteLine(_message);
        return new RequestFilePathContext();
    }
}