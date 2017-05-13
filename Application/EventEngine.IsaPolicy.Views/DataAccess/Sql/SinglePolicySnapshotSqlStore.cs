namespace CodeConcepts.EventEngine.IsaPolicy.Views.DataAccess.Sql
{
    //public class SinglePolicySnapshotSqlStore : ISnapshotStore<PolicyView>
    //{
    //    private static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["Snapshots"].ConnectionString;

    //    private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

    //    public ISnapshot<PolicyView> Get(Guid contextId)
    //    {
    //        using (var connection = new SqlConnection(ConnectionString))
    //        {
    //            connection.Open();
    //            using (var command = new SqlCommand("SelectLatestPolicyViewSnapshot", connection) { CommandType = CommandType.StoredProcedure })
    //            {
    //                command.Parameters.Add(new SqlParameter("@ContextId", SqlDbType.UniqueIdentifier));
    //                command.Parameters["@ContextId"].Value = contextId;
    //                var viewData = (string)command.ExecuteScalar();
    //                return viewData != null ? Deserialize(viewData) : null;
    //            }
    //        }
    //    }

    //    public void Add(PolicyView view, IEvent @event)
    //    {
    //        using (var connection = new SqlConnection(ConnectionString))
    //        {
    //            connection.Open();
    //            using (var command = new SqlCommand("InsertPolicyViewSnapshot", connection) { CommandType = CommandType.StoredProcedure })
    //            {
    //                command.Parameters.Add(new SqlParameter("@ContextId", SqlDbType.UniqueIdentifier));
    //                command.Parameters.Add(new SqlParameter("@DateTime", SqlDbType.DateTime2));
    //                command.Parameters.Add(new SqlParameter("@Data", SqlDbType.NVarChar, -1));

    //                var snapshot = new Snapshot<PolicyView>(@event, view);
    //                var data = Serialize(snapshot);

    //                command.Parameters["@ContextId"].Value = @event.EventContextId;
    //                command.Parameters["@DateTime"].Value = @event.EventDateTime;
    //                command.Parameters["@Data"].Value = data;
    //                command.ExecuteNonQuery();
    //            }
    //        }
    //    }

    //    public void ClearAllSnapshots()
    //    {

    //        using (var connection = new SqlConnection(ConnectionString))
    //        {
    //            connection.Open();
    //            using (var command = new SqlCommand("InsertPolicyViewSnapshot", connection) { CommandType = CommandType.StoredProcedure })
    //            {
    //                throw new NotImplementedException();
    //            }
    //        }
    //    }

    //    private static string Serialize(Snapshot<PolicyView> value)
    //    {
    //        return JsonConvert.SerializeObject(value, JsonSerializerSettings);
    //    }

    //    private Snapshot<PolicyView> Deserialize(string value)
    //    {
    //        return JsonConvert.DeserializeObject<Snapshot<PolicyView>>(value, JsonSerializerSettings);
    //    }
    //}
}