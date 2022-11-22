@echo off
set /p msname=Input name of microservice (e.g. Auth) (e.g. ExchangeRates): 
set /p migrationName=Input name of migration: 
@echo on
dotnet ef migrations add --project:Microservices.%msname%.Data --startup-project Microservices.%msname%.API "%migrationName%"
@echo off
set /p choice=Apply migration to database? [y/n]
@echo on
if %choice%=='y'
	dotnet ef database update --verbose --project Microservices.%msname%.Data --startup-project Microservices.%msname%.API
pause