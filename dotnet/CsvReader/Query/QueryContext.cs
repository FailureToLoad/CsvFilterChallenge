using System.Globalization;

namespace CsvReader.Query;

public class QueryContext : IContext
{
    private readonly KeyValuePair<string,int> _header;
    private readonly CsvDocument _document;
    private const string DateFormat = "yyyyMMdd";

    public QueryContext(KeyValuePair<string,int> header, CsvDocument document)
    {
        _header = header;
        _document = document;
    }
    public IContext Transition()
    {
        string query = null;
        IContext result = null;
        IEnumerable<string[]> results = null;
        if (_header.Key.Contains("name"))
        {
            query = GetQuery(QueryMessages.NameQueryPrompt(_header.Key));
        }
        
        if(_header.Key.Contains("date") || _header.Key.Contains("dob"))
        {
            query = GetQuery(QueryMessages.DateOfBirthQueryPrompt);
            if (!DateOfBirthIsValid(query))
            {
                Console.WriteLine(QueryMessages.InvalidDateOfBirth);
                return this;
            }
        }
        results = ExecuteQuery(_document, _header.Value, query);
        return new QueryResultsAvailableContext(results);
    }

    private string GetQuery(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine()!;
    }

    private bool DateOfBirthIsValid(string dateOfBirth)
    {
        DateTime dob = DateTime.MinValue;
        return DateTime.TryParseExact(dateOfBirth, DateFormat, 
            CultureInfo.InvariantCulture, 
            DateTimeStyles.None, out dob);
        
    }

    private IEnumerable<string[]> ExecuteQuery(CsvDocument document, int index, string query)
    {
        return document.GetRows().Where(rec => rec[index].Equals(query));
    }
}