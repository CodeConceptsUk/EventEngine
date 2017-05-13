using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.DataAccess.Sql
{
   public class SequencingSqlStore : ISequencingRepository
    {
        private static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["Sequencing"].ConnectionString;

        public string Get(string type)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("GetNextSequence", connection) {CommandType = CommandType.StoredProcedure})
                {
                    command.Parameters.Add("@Type", SqlDbType.NVarChar, 255).Value = type;
                    return ((long)command.ExecuteScalar()).ToString();
                }
            }
        }
    }
}