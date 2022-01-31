using System.Globalization;

namespace CsvReader.Query;

public class QueryContext : IContext
{
    private readonly KeyValuePair<string,int> _header;
    private readonly CsvDocument _document;
    private const string DateFormat = "YYYYMMDD";

    public QueryContext(KeyValuePair<string,int> header, CsvDocument document)
    {
        _header = header;
        _document = document;
    }
    public IContext Transition()
    {
        string query = GetQuery(QueryMessages.DateOfBirthQueryPrompt);
        if (!DateOfBirthIsValid(query))
        {
            Console.WriteLine(QueryMessages.InvalidDateOfBirth);
            return this;
        } 
            
        throw new NotImplementedException();
    }

    private string GetQuery(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine()!;
    }

    private bool DateOfBirthIsValid(string dateOfBirth)
    {
        DateTime dob = DateTime.MinValue;
        DateTime.TryParseExact(dateOfBirth, 
            DateFormat, 
            CultureInfo.InvariantCulture, 
            DateTimeStyles.None, 
            out dob);
        return dob > DateTime.MinValue;
    }
}