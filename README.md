# Interest Rate Calculator
Assignment as a part of the System Architect Course (by GeekBrains). The project is focused on practicing CI/CD, therefore the functionality of the application decided to kept simple. 
Main criteria for the project:
- Project should have several apps communicating with each other
- Project should use several programming languages for the apps
- Code should be covered Unit Tests

## Launch instructions
Execute the following commands to run the project:

Build services:

```
docker build -t gateway:v1 -f docker/Dockerfile.GateWay src/Gateway
```

```
docker build -t amortization_calculator:v1 -f docker/Dockerfile.AmortizationCalculator src/AmortizationCalculator
```

```
docker build -t interest_rate_calculator:v1 -f docker/Dockerfile.InterestRateCalculator src/InterestRateCalculator
```

Run the project:
```
docker compose -f docker/docker-compose.yaml -p interest-rate-calculator up -d
```

After that you will be able to test the API documentation via http://localhost:8989/swagger/

## Project structure
    .
    ├── docker                                              # docker files for all services, service discovery, compose file
    │   └── consul                                          # configuration files for HashiCorp Consul service discovery
    └── src                                                 # source code for apps
        ├── AmortizationCalculator                          # service responsible for amortization calculation
        │   ├── AmortizationCalculatorService               # service endpoint (gRPC)
        │   ├── AmortizationCalculatorService.Tests         # Unit Tests for service endpoint and its types
        │   ├── Domain.Tests                                # types and calculation logic for amortization rate
        │   └── Domain                                      # Unit Tests for amortization rate types and calculation logic 
        ├── GateWay                                         # API GateWay that facades calculation services
        │   ├── ServiceClients                              # Implementation of service clients
        │   ├── ServiceClients.Domain                       # Contracts for service clients and data types
        │   ├── ServiceClients.Domain.Tests                 # Unit Tests for service clients data types
        │   ├── WebApi                                      # entrypoint for the project (ASP.NET Core Web API) 
        │   └── WebApi.Tests                                # Unit Tests for API endpoints
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
