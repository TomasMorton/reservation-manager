# Reservation Manager

## Plan
create a reservation for a campaign
  needs to have a date range and the number of faces

input validation:
  date range is valid
  number of faces is positive

business validation:
  start date respects lead time
  number of faces are available

system data required:
  number of faces available per day (capacity)

  
## Dependencies
### Giraffe
While the basic ASP.NET Core api libraries can be used with F#, they're not very functional. This is particularly an issue when trying to compose the app (dependency management).\
Giraffe is a slim wrapper around ASP.NET Core that allows us to build a more functionally designed app.

### DbUp
Used for database creation and migration. See the Database section for more details.

## Database
The Database is created and managed using [DbUp](https://dbup.github.io/).\
The project to manage this is at /tools/SqlMigrations.

To use:
1. Create a SQL server (can be anywhere - below is a guide to setup using docker and MsSql)
1. Configure the connection string for the app. This can be done several ways:
    1. Via appsettings.json. Set the Database:ConnectionString value.
    1. Via environment variables. Set Database:ConnectionString=value.
    1. Via cmd line args. Set Database:ConnectionString=value.
1. Run the app. It will do the following:
    1. If the database doesn't exist, create it.
    1. If the migration scripts have not been run, run them.

### Installing MS SQL Server in Docker
1. docker pull mcr.microsoft.com/mssql/server:2019-latest
1. docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Passw0rd" -p 1433:1433 --name reservation-manager-sql -d mcr.microsoft.com/mssql/server:2019-latest
1. You can connect to sqlcmd using this:
    1. docker exec -it reservation-manager-sql "bash" 
    1. /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P Passw0rd