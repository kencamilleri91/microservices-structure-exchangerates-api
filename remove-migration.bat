@echo off
set /p msname=Input name of microservice (e.g. Auth) (e.g. ExchangeRates) to remove the latest unapplied migration from: 
@echo on
dotnet ef migrations remove --project:Microservices.%msname%.Data --startup-project Microservices.%msname%.API
pause