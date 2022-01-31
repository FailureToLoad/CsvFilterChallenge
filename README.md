# csv-filter-challenge-public

## Assignment
Create a command line application that parses a CSV file and filters the data per user input.

The CSV will contain three fields: `first_name`, `last_name`, and `dob`. The `dob` field will be a date in YYYYMMDD format.

The user should be prompted to filter by `first_name`, `last_name`, or birth year. The application should then accept a name or year and return all records that match the value for the provided filter. 

Example input:
```
first_name,last_name,dob
Bobby,Tables,19700101
Ken,Thompson,19430204
Rob,Pike,19560101
Robert,Griesemer,19640609
```

## How-To


1. Clone the repository to your local machine
2. Navigate to the root directory of the repository via your favorite terminal application.
3. Build the solution via `dotnet build`.
4. Run the solution via `dotnet run`.
5. Follow the prompts.

## Assumptions

1. Queries are always full matches, never partial.
2. Names and Dates will not have commas in them.
3. The program is not required to maintain an activity loop, it simply runs from start to finish.
4. If any CSV rows are missing data, this is an error state that should be handled via providing the user the opportunity to point to a different file.

## Design Choices

I started this by sitting down for a few hours and hammering out the quickest, dirtiest code I could in order to get familiarity with the problem space.
The following became apparent.

1. This application can be represented as two flows, Data Ingress and Data Querying.
2. Using normal exception handling would cause either a poor user experience if exceptions are allowed to bubble up or a poor developer experience if they're swallowed in order to dictate control flow.
3. Using a standard design of creating a CsvReader class that produces a CsvDocument class made for dense testing since much of the internals were obfuscated.

In order to produce code that was highly testable and easily extended, I settled on something akin to the strategy pattern. Diagrams mapping out the strategy transitions are available in the [Ingress](CsvReader/Ingress) and [Query](/CsvReader/Query) folders.
