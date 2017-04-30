using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Application.PropertyBags;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Views.PolicyView;

namespace Policy.Plugin.Isa.Policy.DataAccess.Sql
{
    public class SinglePolicySnapshotSqlStore : ISnapshotStore<PolicyView, IPolicyContext>
    {
        private static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["Snapshots"].ConnectionString;

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public ISnapshot<PolicyView, IPolicyContext> Get(Guid contextId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SelectLatestPolicyViewSnapshot", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add(new SqlParameter("@ContextId", SqlDbType.UniqueIdentifier));
                    var viewData = (string)command.ExecuteScalar();
                    return viewData != null ? Deserialize(viewData) : null;
                }
            }
        }

        public void Add(PolicyView view, IEvent<IPolicyContext> @event)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("InsertPolicyViewSnapshot", connection) { CommandType = CommandType.StoredProcedure })
                {
                    var snapshot = new Snapshot<PolicyView, IPolicyContext>(@event, view);
                    var data = Serialize(snapshot);

                    command.Parameters["@ContextId"].Value = @event.EventContextId;
                    command.Parameters["@DateTime"].Value = @event.EventDateTime;
                    command.Parameters["@Data"].Value = data;
                    command.ExecuteNonQuery();
                }
            }
        }

        private static string Serialize(Snapshot<PolicyView, IPolicyContext> value)
        {
            return JsonConvert.SerializeObject(value, JsonSerializerSettings);
        }

        private Snapshot<PolicyView, IPolicyContext> Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<Snapshot<PolicyView, IPolicyContext>>(value, JsonSerializerSettings);
        }
    }
}