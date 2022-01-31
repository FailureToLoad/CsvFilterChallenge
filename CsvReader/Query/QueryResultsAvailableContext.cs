namespace CsvReader.Query;

public class QueryResultsAvailableContext : IContext
{
    private readonly IEnumerable<string[]> _results;
    public QueryResultsAvailableContext(IEnumerable<string[]> results) => _results = results;
    public IContext Transition()
    {
        if (!_results.Any())
            Console.WriteLine(QueryMessages.NoResults);
        else
            DisplayResults(_results);
        return this;
    }

    private void DisplayResults(IEnumerable<string[]> results)
    {
        foreach (var record in results)
        {
            Console.WriteLine(string.Join(" ", record));
        }
    }
}