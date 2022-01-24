# Interest Rate Calculator
Assignment as a part of the System Architect Course (by GeekBrains). The project is focused on practicing CI/CD, therefore the functionality of the application decided to kept simple. 
Main criteria for the project:
- Project should have several apps communicating with each other
- Project should use several programming languages for the apps
- Code should be covered Unit Tests

## Project structure
    .
    └── src                                                 # source code for apps
        ├── AmortizationCalculator                          # service responsible for amortization calculation
        │   ├── AmortizationCalculatorService               # service endpoint (gRPC)
        │   ├── Domain.Tests                                # types and calculation logic for amortization rate
        │   └── Domain                                      # Unit Tests for amortization rate types and calculation logic 
        ├── GateWay                                         # API GateWay that facades calculation services
        │   ├── WebApi                                      # entrypoint for the project (ASP.NET Core Web API) 
        │   └── WebApi.Tests                                # Unit Tests
        └── InterestRateCalculator                          # service responsible for interest rate calculation
            ├── Domain.Tests                                # Unit Tests for interest rate types and calculation logic
            ├── Domain                                      # types and calculation logic for interest rate
            ├── InterestRateCalculatorService.Tests         # Unit Tests for service endpoint and its types
            └── InterestRateCalculatorService               # service endpoint (ASP.NET Core Web API)
    
## Branches
GitFlow is used as branching model
- main: Contains latest stable release 
- develop: All ongoing Development 
- feature/: Each task should be solved in separate feature branch 


## Commit naming convention
[Conventional commits](https://www.conventionalcommits.org/en/v1.0.0/) convention must be followed.
