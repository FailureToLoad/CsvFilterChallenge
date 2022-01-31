using System;
using System.IO;
using CsvReader.Ingress;
using NUnit.Framework;

namespace CsvReader.Tests;

public class IngressTests
{
    private const string MissingFilePath = "garbage";

    [Test]
    public void When_UserInputsMissingFilePath_Then_ReturnToStartingPoint()
    {
        Console.SetIn(new StringReader(MissingFilePath));
        IContext context = new RequestFilePathContext();
        context = context.Transition();
        Assert.True(context is RequestFilePathContext);
    }
}