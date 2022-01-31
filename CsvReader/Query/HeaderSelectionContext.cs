namespace CsvReader.Query;

public class HeaderSelectionContext : IContext
{
    private readonly CsvDocument _document;
    public HeaderSelectionContext(CsvDocument document) => _document = document;

    public IContext Transition()
    {
        Console.WriteLine(QueryMessages.HeaderSelection);
        var headers = _document.GetHeaders().OrderBy(kvp => kvp.Value);
        foreach (KeyValuePair<string, int> kvp in headers)
            Console.WriteLine($"{kvp.Value}.{kvp.Key+1}");
        string? choice = Console.ReadLine()!;
        if (int.TryParse(choice, out int friend))
            return new QueryRequestContext(friend);
        return this;
    }
}