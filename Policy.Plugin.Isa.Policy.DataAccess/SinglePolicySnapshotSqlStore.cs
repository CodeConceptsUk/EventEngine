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

namespace Policy.Plugin.Isa.Policy.DataAccess
{
    public class SinglePolicySnapshotSqlStore : ISnapshotStore<PolicyView, IPolicyContext>
    {
        private static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["Snapshots"]
            .ConnectionString;

        private static readonly JsonSerializerSettings JsonSerializerSettings =
            new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};

        private static readonly SqlConnection Connection = new SqlConnection(ConnectionString);

        static SinglePolicySnapshotSqlStore()
        {
            Connection.Open();
            SelectCommand = new SqlCommand("SelectLatestPolicyViewSnapshot", Connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            SelectCommand.Parameters.Add(new SqlParameter("@ContextId", SqlDbType.UniqueIdentifier));

            InsertCommand = new SqlCommand("InsertPolicyViewSnapshot", Connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            InsertCommand.Parameters.Add(new SqlParameter("@ContextId", SqlDbType.UniqueIdentifier));
            InsertCommand.Parameters.Add(new SqlParameter("@DateTime", SqlDbType.DateTime2));
            InsertCommand.Parameters.Add(new SqlParameter("@Data", SqlDbType.NVarChar, -1));
        }

        private static SqlCommand InsertCommand { get; }

        private static SqlCommand SelectCommand { get; }

        public ISnapshot<PolicyView, IPolicyContext> Get(Guid contextId)
        {
            SelectCommand.Parameters["@ContextId"].Value = contextId;
            var viewData = (string) SelectCommand.ExecuteScalar();
            return viewData != null ? Deserialize(viewData) : null;
        }

        public void Add(PolicyView view, IEvent<IPolicyContext> @event)
        {
            var snapshot = new Snapshot<PolicyView, IPolicyContext>(@event, view);
            var data = Serialize(snapshot);
            InsertCommand.Parameters["@ContextId"].Value = @event.EventContextId;
            InsertCommand.Parameters["@DateTime"].Value = @event.EventDateTime;
            InsertCommand.Parameters["@Data"].Value = data;
            InsertCommand.ExecuteNonQuery();
        }


        private string Serialize(Snapshot<PolicyView, IPolicyContext> value)
        {
            return JsonConvert.SerializeObject(value, JsonSerializerSettings);
        }

        private Snapshot<PolicyView, IPolicyContext> Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<Snapshot<PolicyView, IPolicyContext>>(value, JsonSerializerSettings);
        }
    }
}