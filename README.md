# Interest Rate Calculator
Assignment as a part of the System Architect Course (by GeekBrains). The project is focused on practicing CI/CD, therefore the functionality of the application decided to kept simple. 
Main criteria for the project:
- Project should have several apps communicating with each other
- Project should use several programming languages for the apps
- Code should be covered Unit Tests

## Project structure
    .
    ├── src                                                 # source code for apps
    │   ├── InterestRateCalculator                          # service responsible for interest rate calculation
    │   │   ├── Domain                                      # types and calculation logic for interest rate
    │   │   ├── Domain.Tests                                # Unit Tests
    │   │   └── InterestRateCalculatorService               # service endpoint (ASP.NET Core Web API)
    │   ├── AmortizationCalculator                          # service responsible for amortization calculation
    │   │   ├── AmortizationCalculatorService               # service endpoint (gRPC)
    │   │   ├── Domain                                      # types and calculation logic for amortization rate
    │   │   └── Domain.Tests                                # Unit Tests
    │   └── GateWay                                         # API GateWay that facades calculation services
    │   │   ├── WebApi                                      # entrypoint for the project (ASP.NET Core Web API)
    │   │   └── WebApi.Tests                                # Unit Tests
    
## Branches
GitFlow is used as branching model
- main: Contains latest stable release 
- develop: All ongoing Development 
- feature/: Each task should be solved in separate feature branch 


## Commit naming convention
[Conventional commits](https://www.conventionalcommits.org/en/v1.0.0/) convention must be followed.
