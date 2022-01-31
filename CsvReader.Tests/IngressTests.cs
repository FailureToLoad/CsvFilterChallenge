using System;
using System.IO;
using CsvReader.Ingress;
using NUnit.Framework;

namespace CsvReader.Tests;

public class IngressTests
{
    private const string MissingFilePath = "garbage";
    private const string ValidFilePath = "valid.csv";
    private const string EmptyFilePath = "empty.csv";

    [Test]
    public void When_UserInputsMissingFilePath_Then_RequestNewFile()
    {
        Console.SetIn(new StringReader(MissingFilePath));
        IContext context = new RequestFilePathContext();
        context = context.Transition();
        Assert.True(context is RequestFilePathContext);
    }
    
    [Test]
    public void When_UserInputsValidFilePath_Then_AdvanceToFileProcessing()
    {
        Console.SetIn(new StringReader(ValidFilePath));
        IContext context = new RequestFilePathContext();
        context = context.Transition();
        Assert.True(context is FileProcessingContext);
    }
    
    [Test]
    public void When_UserProvidesEmptyFile_Then_ReportError()
    {
        IContext context = new FileProcessingContext(EmptyFilePath);
        context = context.Transition();
        Assert.True(context is ErrorReportingContext);
    }
    
}