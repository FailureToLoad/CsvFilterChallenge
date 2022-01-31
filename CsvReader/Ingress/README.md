# Ingress

I started this by sitting down for a few hours and hammering out the quickest, dirtiest code I could in order to get familiarity with the problem space.
The following became apparent.

1. This application can be represented as two flows, Data Ingress and Data Querying.
2. Using normal exception handling would cause either a poor user experience if exceptions are allowed to bubble up or a poor developer experience if they're swallowed in order to dictate control flow.
3. Using a standard design of creating a CsvReader class that produces a CsvDocument class made for dense testing since much of the internals were obfuscated.

In order to produce code that was highly testable and easily extended, I settled on something akin to the strategy pattern.  
Below is a rough diagram for the ingress strategy transitions.

[![](https://mermaid.ink/img/pako:eNptkltrwjAYhv9KyFUH-gd6saG2tR4Z3RiMdReh-dRAm3Q5sEn1vy-JiTpYr9I8z_fm7WHAjaCAU7yXpD-g16zmyF6Tjwq-DChdsBY-0Xj8iKaDW6P8hymtns4Xb-rRaStOaJY4vhW6EIbThwufeT75I7-DOmXJsxQNKOVmgpt5nEdSAqEgVYC5h8UQdi81YoviFjyP42-kNVCJ7xhQXJuWvmje9foYWHnfcu5vFoObRTaF0XjM4hqxTAohO6JzKYUMIcv7kJu6SlzQhinF-D6oq39UV36dZKIxHXBd2ceM7dZe2FjG46uqOR7hDmwFRu23G9xujfUBOqhxapcUdsS0usY1P1vV9JRoyCnTQuJ0R1oFI0yMFi9H3uBUSwNRyhixv0IXrPMvPPOkig)](https://mermaid-js.github.io/mermaid-live-editor/edit/#pako:eNptkltrwjAYhv9KyFUH-gd6saG2tR4Z3RiMdReh-dRAm3Q5sEn1vy-JiTpYr9I8z_fm7WHAjaCAU7yXpD-g16zmyF6Tjwq-DChdsBY-0Xj8iKaDW6P8hymtns4Xb-rRaStOaJY4vhW6EIbThwufeT75I7-DOmXJsxQNKOVmgpt5nEdSAqEgVYC5h8UQdi81YoviFjyP42-kNVCJ7xhQXJuWvmje9foYWHnfcu5vFoObRTaF0XjM4hqxTAohO6JzKYUMIcv7kJu6SlzQhinF-D6oq39UV36dZKIxHXBd2ceM7dZe2FjG46uqOR7hDmwFRu23G9xujfUBOqhxapcUdsS0usY1P1vV9JRoyCnTQuJ0R1oFI0yMFi9H3uBUSwNRyhixv0IXrPMvPPOkig)