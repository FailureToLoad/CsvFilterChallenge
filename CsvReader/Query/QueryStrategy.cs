namespace CsvReader.Query;

public class QueryStrategy
{
    public IEnumerable<string[]> QueryDocument(CsvDocument document)
    {
        IContext context = new HeaderSelectionContext(document);
        while (context is not QueryResultsAvailableContext)
            context = context.Transition();
        QueryResultsAvailableContext endState = (context as QueryResultsAvailableContext)!;
        return endState.GetResults();
    }
}