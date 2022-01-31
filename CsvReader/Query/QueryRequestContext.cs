namespace CsvReader.Query;

public class QueryRequestContext : IContext
{
    private readonly int _index;
    public QueryRequestContext(int index) => _index = index;
    public IContext Transition()
    {
        throw new NotImplementedException();
    }
}