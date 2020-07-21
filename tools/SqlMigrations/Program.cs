using System;
using System.Reflection;
using DbUp;
using DbUp.Engine;
using Microsoft.Extensions.Configuration;

namespace SqlMigrations
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var config = GetConfiguration(args);
            var dbConfig = GetDatabaseOptions(config);

            CreateDatabase(dbConfig);
            var upgradeResult = UpgradeDatabase(dbConfig);
            return upgradeResult;
        }

        private static void CreateDatabase(DatabaseOptions dbConfig)
        {
            Console.WriteLine("Creating database (if it doesn't exist)...");
            EnsureDatabase.For.SqlDatabase(dbConfig.ConnectionString);
        }

        private static int UpgradeDatabase(DatabaseOptions dbConfig)
        {
            Console.WriteLine("Checking for database upgrade...");
            var upgrader = CreateDatabaseUpgrader(dbConfig);

            if (!upgrader.IsUpgradeRequired())
            {
                Console.WriteLine("Database is already up to date.");
                return 0;
            }

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.Error.WriteLine(result.Error);
                return -1;
            }
            else
            {
                Console.WriteLine("Database upgraded successfully");
                return 0;
            }
        }

        private static UpgradeEngine CreateDatabaseUpgrader(DatabaseOptions dbConfig)
        {
            return DeployChanges.To
                .SqlDatabase(dbConfig.ConnectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();
        }

        private static IConfigurationRoot GetConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }

        private static DatabaseOptions GetDatabaseOptions(IConfiguration config)
        {
            return config.GetSection("Database").Get<DatabaseOptions>();
        }
    }
}