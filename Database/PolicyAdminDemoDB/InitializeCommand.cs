using System.Data.SqlClient;
using System.Reflection;
using DbUp;
using ManyConsole;
using log4net;

namespace DemoDB
{
    public class InitializeCommand : ConsoleCommand
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public bool CreateDatabase { get; set; }

        public bool ReplaceDatabase { get; set; }

        public InitializeCommand()
        {
            IsCommand("Initialize", "Initialize the database ready for use");

            SkipsCommandSummaryBeforeRunning();

            HasLongDescription("Initializes the database ready for use assuming you have permission to create/update the database using your windows credentials");
            
            HasRequiredOption("s|server=", "The hostname / ip address of the server", p => Server = p);

            HasOption("d|database=", "The database name to use", p => Database = p);

            HasOption("c|createdb", "Should we create the database if it isn't found?", p => { CreateDatabase = true; } );

            HasOption("replace", "Should we replace the database if it is found?", p => { ReplaceDatabase = true; } );
        }

        public override int Run(string[] remainingArguments)
        {
            if (string.IsNullOrWhiteSpace(Database))
            {
                Database = "PolicyAdminDemoDB";
            }
            var logger = LogManager.GetLogger(nameof(InitializeCommand));
            logger.Info($"Executing Initialize for {Database} on {Server}");

            if (ReplaceDatabase)
            {
                RemoveOldDatabase();
            }
            var connectionString = $"Server={Server}; Database={Database}; Trusted_connection=true";
            if (CreateDatabase)
            {
                logger.Info($"Attempting to create database {Database}");
                EnsureDatabase.For.SqlDatabase(connectionString);
            }
            var upgradeLogger = new UpgradeLogger(logger);
            var upgrader = DeployChanges.To.SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogTo(upgradeLogger)
                .WithTransaction()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (result.Successful)
            {
                logger.Info("Successfully executed:");
                //foreach (var resultScript in result.Scripts)
                //{
                //    logger.Info($"\t{resultScript.Name}");
                //}
            }
            else
            {
                logger.Error(result.Error);
            }
            return 0;
        }

        private void RemoveOldDatabase()
        {
            LogManager.GetLogger(nameof(InitializeCommand)).Warn($"Deleting {Database}");
            var connectionString = $"Server={Server}; Database=master; Trusted_connection=true";
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand($"IF EXISTS(select * from sys.databases where name='{Database}') DROP DATABASE {Database}"))
                {
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
