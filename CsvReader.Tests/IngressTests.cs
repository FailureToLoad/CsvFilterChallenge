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
    private const string HeaderElementMistmatch = "header-element-mismatch.csv";

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
    public void When_ProcessingEmptyFile_Then_ReportError()
    {
        IContext context = new FileProcessingContext(EmptyFilePath);
        context = context.Transition();
        Assert.True(context is ErrorReportingContext);
    }

    [Test]
    public void When_ProcessingFileWithOnlyHeader_Then_ReportError()
    {
        IContext context = new FileProcessingContext(HeaderElementMistmatch);
        context = context.Transition();
        Assert.True(context is ErrorReportingContext);
    }
    
}