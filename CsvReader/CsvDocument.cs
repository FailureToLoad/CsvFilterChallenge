namespace CsvReader;

public class CsvDocument
{
    private Dictionary<string,int> HeaderMap { get; }
    private IEnumerable<string[]> ValueRows { get; }

    public CsvDocument(Dictionary<string, int> headerMap, IEnumerable<string[]> valueRows)
    {
        HeaderMap = headerMap;
        ValueRows = valueRows;
    }

    public IEnumerable<string[]> GetRows() => ValueRows;
    public Dictionary<string,int> GetHeaders() => HeaderMap;
}