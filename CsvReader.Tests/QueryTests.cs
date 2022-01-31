using System;
using System.Collections.Generic;
using System.IO;
using CsvReader.Query;
using NUnit.Framework;

namespace CsvReader.Tests;


public class QueryTests
{
    private const string InvalidDobQuery = "1/11/2021";
    private const string ValidDobQuery = "19700101";
    [Test]
    public void When_UserSubmitsValidChoice_Then_AdvanceToQueryRequestState()
    {
        Console.SetIn(new StringReader("1"));
        var document = MakeValidDocument();
        IContext context = new HeaderSelectionContext(document);
        context = context.Transition();
        Assert.IsInstanceOf<QueryContext>(context);
    }

    [TestCase("skub")]
    [TestCase("10")]
    public void When_UserSumbitsInvalidChoice_Then_ReissueHeaderRequest(string choice)
    {
        Console.SetIn(new StringReader(choice));
        var document = MakeValidDocument();
        IContext context = new HeaderSelectionContext(document);
        context = context.Transition();
        Assert.IsInstanceOf<HeaderSelectionContext>(context);
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
    public void When_ValidDobQueryReceived_Then_AdvanceToQueryResultsAvailableContext()
    {
        Console.SetIn(new StringReader(ValidDobQuery));
        KeyValuePair<string, int> header = GetDobHeader(); 
        var document = MakeValidDocument();
        IContext context = new QueryContext(header, document);
        context = context.Transition();
        Assert.IsInstanceOf<QueryContext>(context);
    }

    private CsvDocument MakeValidDocument()
    {
        Dictionary<string, int> headers = new()
        {
            {"first_name", 0},
            {"last_name", 1},
            {"dob", 2}
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
}