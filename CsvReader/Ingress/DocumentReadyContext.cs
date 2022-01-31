namespace CsvReader.Ingress;

public class DocumentReadyContext : IContext
{
    private readonly CsvDocument _cvsDocument;
    public DocumentReadyContext(CsvDocument document) => _cvsDocument = document;
    public IContext Transition()
    {
        Console.WriteLine(IngressMessages.DocumentReady);
        return this;
    }

    public CsvDocument GetDocument()
    {
        return _cvsDocument;
    }
}