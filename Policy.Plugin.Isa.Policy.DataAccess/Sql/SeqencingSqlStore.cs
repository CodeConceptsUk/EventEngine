using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;

namespace Policy.Plugin.Isa.Policy.DataAccess.Sql
{
   public class SeqencingSqlStore : ISequencingRepository
    {
        private static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["Seqencing"].ConnectionString;

        public string Get(string type)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("GetNextSeqenece") {CommandType = CommandType.StoredProcedure})
                {
                    command.Parameters.Add("@Type", SqlDbType.NVarChar, 255).Value = type;
                    return (string)command.ExecuteScalar();
                }
            }
        }
    }
}
