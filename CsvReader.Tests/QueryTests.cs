using System;
using System.Collections.Generic;
using System.IO;
using CsvReader.Query;
using NUnit.Framework;

namespace CsvReader.Tests;


public class QueryTests
{
    [Test]
    public void When_UserSubmitsValidChoice_Then_AdvanceToQueryRequestState()
    {
        Console.SetIn(new StringReader("1"));
        var document = MakeValidDocument();
        IContext context = new HeaderSelectionContext(document);
        context = context.Transition();
        Assert.IsInstanceOf<QueryRequestContext>(context);
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
}