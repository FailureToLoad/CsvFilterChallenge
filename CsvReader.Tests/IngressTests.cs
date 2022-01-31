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
    private const string NoValueRows = "no-value-rows.csv";

    [Test]
    public void When_UserInputsMissingFilePath_Then_RequestNewFile()
    {
        Console.SetIn(new StringReader(MissingFilePath));
        IContext context = new RequestFilePathContext();
        context = context.Transition();
        Assert.IsInstanceOf(typeof(RequestFilePathContext), context);
    }
    
    [Test]
    public void When_UserInputsValidFilePath_Then_AdvanceToFileProcessing()
    {
        Console.SetIn(new StringReader(ValidFilePath));
        IContext context = new RequestFilePathContext();
        context = context.Transition();
        Assert.IsInstanceOf(typeof(FileProcessingContext), context);
    }
    
    [TestCase(EmptyFilePath)]
    [TestCase(HeaderElementMistmatch)]
    [TestCase(NoValueRows)]
    public void When_ProcessingInvalidFile_Then_ReportError(string filePath)
    {
        IContext context = new FileProcessingContext(filePath);
        context = context.Transition();
        Assert.IsInstanceOf(typeof(ErrorReportingContext), context);
    }

    [Test]
    public void After_ErrorReported_Then_RequestNewFile()
    {
        IContext context = new ErrorReportingContext("");
        context = context.Transition();
        Assert.IsInstanceOf(typeof(RequestFilePathContext), context);
    }

    [Test]
    public void When_ProcessingValidFile_Then_ReportDocumentReady()
    {
        IContext context = new FileProcessingContext(ValidFilePath);
        context = context.Transition();
        Assert.IsInstanceOf(typeof(DocumentReadyContext), context);
    }
}