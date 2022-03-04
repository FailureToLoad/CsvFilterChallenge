namespace CsvReader.Query;

public class ColumnSelectionContext : IContext
{
    private readonly CsvDocument _document;
    public ColumnSelectionContext(CsvDocument document) => _document = document;

    public IContext Transition()
    {
        Console.WriteLine(QueryMessages.ColumnSelection);
        var headers = _document.GetHeaders().OrderBy(kvp => kvp.Value);
        foreach (KeyValuePair<string, int> kvp in headers)
            Console.WriteLine($"{kvp.Value+1}.{kvp.Key}");
        string choice = Console.ReadLine()!;
        if (int.TryParse(choice, out int parsedChoice) && IsChoiceInRange(parsedChoice--,_document.GetHeaders().Values))
            return new QueryContext(headers.First(kvp => kvp.Value == parsedChoice), _document);
        return this;
    }

    public bool IsChoiceInRange(int choice, IEnumerable<int> indecies)
    {
        return choice > indecies.Min() && choice < indecies.Max();
    }
}