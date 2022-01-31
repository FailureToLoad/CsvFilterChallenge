namespace CsvReader.Query;

public class QueryResultsAvailableContext : IContext
{
    private readonly IEnumerable<string[]> _results;
    public QueryResultsAvailableContext(IEnumerable<string[]> results) => _results = results;
    public IContext Transition()
    {
        if (!_results.Any())
            Console.WriteLine(QueryMessages.NoResults);
        return this;
    }

    public IEnumerable<string[]> GetResults() => _results;
}