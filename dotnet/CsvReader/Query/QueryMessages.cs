namespace CsvReader.Query;

public static class QueryMessages
{
    public static string ColumnSelection => "Please select the column you wish to query.";
    public static string DateOfBirthQueryPrompt => "Please enter a date of birth in YYYYMMDD format.";
    public static string NameQueryPrompt(string nameHeader) => $"Please enter a {nameHeader.Replace("_", " ")} to search for.";
    public static string InvalidDateOfBirth => "Date entered was not in YYYYMMDD format, please try again";
    public static string NoResults => "Query returned no results. Try again? (Y/N)";
}