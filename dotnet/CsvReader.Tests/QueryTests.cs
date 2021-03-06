using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvReader.Query;
using NUnit.Framework;

namespace CsvReader.Tests;


public class QueryTests
{
    private const string InvalidDobQuery = "1/11/2021";
    private const string ValidDobQuery = "19700101";
    private const string FirstNameQuery = "Bobby";
    private const string LastNameQuery = "Tables";
    [Test]
    public void When_UserSubmitsValidChoice_Then_AdvanceToQueryRequest()
    {
        Console.SetIn(new StringReader("1"));
        var document = MakeValidDocument();
        IContext context = new ColumnSelectionContext(document);
        context = context.Transition();
        Assert.IsInstanceOf<QueryContext>(context);
    }

    [TestCase("skub")]
    [TestCase("10")]
    public void When_UserSumbitsInvalidChoice_Then_ReissueHeaderRequest(string choice)
    {
        Console.SetIn(new StringReader(choice));
        var document = MakeValidDocument();
        IContext context = new ColumnSelectionContext(document);
        context = context.Transition();
        Assert.IsInstanceOf<ColumnSelectionContext>(context);
    }

    [Test]
    public void When_InvalidDobQueryReceived_Then_RepromptForQuery()
    {
        Console.SetIn(new StringReader(InvalidDobQuery));
        KeyValuePair<string, int> header = GetDobHeader(); 
        var document = MakeValidDocument();
        IContext context = new QueryContext(header, document);
        context = context.Transition();
        Assert.IsInstanceOf<QueryContext>(context);
    }
    
    [Test]
    public void When_ValidDobQueryReceived_Then_AdvanceToQueryResultsAvailable()
    {
        Console.SetIn(new StringReader(ValidDobQuery));
        KeyValuePair<string, int> header = GetDobHeader(); 
        var document = MakeValidDocument();
        IContext context = new QueryContext(header, document);
        context = context.Transition();
        Assert.IsInstanceOf<QueryResultsAvailableContext>(context);
    }

    [Test]
    public void When_FirstNameQueryReceived_Then_AdvanceToQueryResultsAvailable()
    {
        Console.SetIn(new StringReader(FirstNameQuery));
        KeyValuePair<string, int> header = GetFirstNameHeader(); 
        var document = MakeValidDocument();
        IContext context = new QueryContext(header, document);
        context = context.Transition();
        Assert.IsInstanceOf<QueryResultsAvailableContext>(context);
    }
    
    [Test]
    public void When_LastNameQueryReceived_Then_AdvanceToQueryResultsAvailable()
    {
        Console.SetIn(new StringReader(LastNameQuery));
        KeyValuePair<string, int> header = GetLastNameHeader(); 
        var document = MakeValidDocument();
        IContext context = new QueryContext(header, document);
        context = context.Transition();
        Assert.IsInstanceOf<QueryResultsAvailableContext>(context);
    }
    
    [Test]
    public void When_QueryStrategyTakesValidInput_Then_QueryReturnsResults()
    {
        var data = String.Join(Environment.NewLine, new[]
        {
            "1",
            "Bobby",
        });
        Console.SetIn(new StringReader(data));
        QueryStrategy strategy = new();
        var results = strategy.QueryDocument(MakeValidDocument());
        Assert.IsNotEmpty(results);
    }
    
    [Test]
    public void When_QueryStrategyTakesValidInput_Then_ReturnedResultsAreValid()
    {
        var data = String.Join(Environment.NewLine, new[]
        {
            "1",
            "Bobby",
        });
        Console.SetIn(new StringReader(data));
        QueryStrategy strategy = new();
        var result = strategy.QueryDocument(MakeValidDocument()).First();
        var expected = RequestMrTables();
        Assert.AreEqual(expected,result);
    }

    private CsvDocument MakeValidDocument()
    {
        Dictionary<string, int> headers = new()
        {
            {"first_name", 0},
            {"last_name", 1},
            {"date_of_birth", 2}
        };

        List<string[]> rows = new()
        {
            new[] {"Bobby","Tables","19700101"},
            new[] {"Ken","Thompson","19430204"},
            new[] {"Rob","Pike","19560101"},
            new[] {"Robert","Griesemer","19640609"}
        };
        return new CsvDocument(headers, rows);
    }

    private KeyValuePair<string, int> GetDobHeader()
    {
        return new KeyValuePair<string, int>("dob", 2);
    }
    
    private KeyValuePair<string, int> GetFirstNameHeader()
    {
        return new KeyValuePair<string, int>("first_name", 0);
    }
    
    private KeyValuePair<string, int> GetLastNameHeader()
    {
        return new KeyValuePair<string, int>("last_name", 1);
    }

    private string[] RequestMrTables()
    {
        return new[] {"Bobby", "Tables", "19700101"};
    }
}