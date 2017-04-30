using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using FrameworkExtensions.LinqExtensions;
using Newtonsoft.Json;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Application.PropertyBags;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Views.PolicyView;

namespace Policy.Plugin.Isa.Policy.DataAccess.Sql
{
    public class PolicyContextEventStoreInMemoryStore : IEventStoreRepository<IPolicyContext>
    {
        private static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["Events"].ConnectionString;

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        private static readonly IList<IEvent<IPolicyContext>> Events = new List<IEvent<IPolicyContext>>();

        public IEnumerable<Guid> FindContextIds(Expression<Func<IEvent<IPolicyContext>, bool>> @where)
        {
            throw new NotImplementedException();
            var expression = @where.Compile();
            var events = Events.Where(t => expression(t)).Select(t => t.EventContextId);
            return events;
        }

        public IEnumerable<IEvent<IPolicyContext>> Get(Guid eventContextId, Guid? eventId = null)
        {
            throw new NotImplementedException();
            if (eventId.HasValue)
            {
                var eventIndex = Events.IndexOf(Events.FirstOrDefault(t => t.EventId == eventId));
                if (eventIndex != -1)
                    return Events.Skip(eventIndex + 1);

                throw new Exception($"Event {eventId} not found in the event store.");
            }

            return Events.Where(t => t.EventContextId == eventContextId);


            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SelectLatestPolicyViewSnapshot", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add(new SqlParameter("@ContextId", SqlDbType.UniqueIdentifier));
                   // command.Parameters["@ContextId"].Value = contextId;
                    var viewData = (string)command.ExecuteScalar();
                   // return viewData != null ? Deserialize(viewData) : null;
                }
            }
        }

        public void Add(IEnumerable<IEvent<IPolicyContext>> events)
        {
            throw new NotImplementedException();
            events.ForEach(t => Events.Add(t));


            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("InsertPolicyViewSnapshot", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add(new SqlParameter("@ContextId", SqlDbType.UniqueIdentifier));
                    command.Parameters.Add(new SqlParameter("@DateTime", SqlDbType.DateTime2));
                    command.Parameters.Add(new SqlParameter("@Data", SqlDbType.NVarChar, -1));

                   // var snapshot = new Snapshot<PolicyView, IPolicyContext>(@event, view);
                   // var data = Serialize(snapshot);

                   // command.Parameters["@ContextId"].Value = @event.EventContextId;
                   // command.Parameters["@DateTime"].Value = @event.EventDateTime;
                   // command.Parameters["@Data"].Value = data;
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