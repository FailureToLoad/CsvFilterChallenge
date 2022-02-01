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

This program will parse a csv file. Prior to running it, a csv filepath should be copied and ready to be pasted.  
If you do not have a csv file for testing, a sample input file is provided on the releases page.  

### Windows

1. Download the version of the application for your operating system from the releases page.
2. Unzip the files to the location of your choice.
3. Double click the CsvReader.exe file to launch the program.
4. Follow the prompts.

### OSX/Linux

1. Download the version of the application for your operating system from the releases page.
2. Unzip the files to the location of your choice.
3. In OSX/Linux, open a terminal window and navigate to where the files were extracted.
5. Ensure the file is executable by running `chmod +x ./CsvReader`
6. Run the program with `./CsvReader`
7. Follow the prompts.

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

In order to produce code that was highly testable and easily extended, I settled on something akin to the strategy pattern. This was done for the following reasons.

### At-Least-Once Completion
While an activity loop was assumed to not be required, I still wanted to make sure the user made it to the end at least once. This meant being able to gracefully resume after hitting error states. Performing a strategy enables different user contexts to return the appropriate next link in the chain. Simply put, it gave me better tools to recover from errors.

### Testability
The biggest benefit was testing. Since my strategies are compositions of states I can test my system by observing state transitions. This allows me to test each state transition as a unit. Doing this helps me avoid asserting on the structure of my system and instead keeps me solely focused on asserting on the behavior of the system.

### Extensability
My system has achieved a fair balance of cohesion and coupling by favoring composition of states to represent a user flow. This results in less rewriting as usecases change. This is expressed particularly well in the Ingress path. The error reporting was centralized in its own context. This allows for re-use of a proven unit of functionality for any ingress scenarios that may be introduced. A developer can focus on the core requirement of the feature they're working on and utilize existing states for more general functionality. As time goes on and more functionality is added, this situation continually improves as more states are made available.


Diagrams mapping out the strategy transitions are available in the [Ingress](CsvReader/Ingress/README.md) and [Query](/CsvReader/Query/README.md) folders.
