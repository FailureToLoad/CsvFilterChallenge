namespace CsvReader.Ingress;

public class IngressStrategy
{
    public CsvDocument ReadCsvFromFile()
    {
        IContext context = new RequestFilePathContext();
        while (context is not DocumentReadyContext)
            context = context.Transition();
        DocumentReadyContext endState = (context as DocumentReadyContext)!;
        return endState.GetDocument();
    }
}