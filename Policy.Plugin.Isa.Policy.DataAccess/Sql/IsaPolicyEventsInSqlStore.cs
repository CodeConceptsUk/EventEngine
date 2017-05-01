using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;

namespace Policy.Plugin.Isa.Policy.DataAccess.Sql
{
    public class IsaPolicyEventsInSqlStore : IIsaPolicyEventStoreRepository
    {
        private static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["IsaPolicyEventStore"].ConnectionString;
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public Guid FindContextIds(string policyNumber)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("select [EventContextId] from [IsaPolicyEvents] where jsonPolicyNumber = @PolicyNumber", connection))
                {
                    command.Parameters.Add("@PolicyNumber", SqlDbType.NVarChar, 255).Value = policyNumber;
                    return (Guid)command.ExecuteScalar();
                }
            }
        }

        public IEnumerable<Guid> FindContextIds(int customerId)
        {
            var contextIds = new List<Guid>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("select [EventContextId] from [IsaPolicyEvents] where jsonCustomerId = @CustomerId", connection))
                {
                    command.Parameters.Add("@CustomerId", SqlDbType.NVarChar, 255).Value = customerId;
                    using (var reader = command.ExecuteReader())
                    {
                        contextIds.Add((Guid)reader[0]);
                    }
                }
            }
            return contextIds;
        }

        public IEnumerable<IsaPolicyEvent> Get(Guid eventContextId, Guid? afterEventId = null)
        {
            return afterEventId.HasValue
                ? GetIsaPolicyEventsForEventContextIdAfterEventId(eventContextId, afterEventId.Value)
                : GetIsaPolicyEventsForEventContextId(eventContextId);
        }

        private static IEnumerable<IsaPolicyEvent> GetIsaPolicyEventsForEventContextId(Guid eventContextId)
        {
            var events = new List<IsaPolicyEvent>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"select * from [IsaPolicyEvents] where [EventContextId] = @EventContextId", connection) { CommandType = CommandType.Text })
                {
                    command.Parameters.Add("@EventContextId", SqlDbType.UniqueIdentifier).Value = eventContextId;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            events.Add(Deserialize((string)reader["data"]));
                    }
                }
            }
            return events;
        }

        private static IEnumerable<IsaPolicyEvent> GetIsaPolicyEventsForEventContextIdAfterEventId(Guid eventContextId, Guid afterEventId)
        {
            var events = new List<IsaPolicyEvent>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"select 
                                                        * 
                                                    from [IsaPolicyEvents] 
                                                    where 
                                                            [EventContextId] = @EventContextId 
                                                        and [SequenceId] > (select [SequenceId] from [IsaPolicyEvents] where [EventId] = @EventId)", connection) { CommandType = CommandType.Text })
                {
                    command.Parameters.Add("@EventContextId", SqlDbType.UniqueIdentifier).Value = eventContextId;
                    command.Parameters.Add("@EventId", SqlDbType.UniqueIdentifier).Value = afterEventId;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            events.Add(Deserialize((string)reader["data"]));
                    }
                }
            }
            return events;
        }

        public void Add(IEnumerable<IsaPolicyEvent> events)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                foreach (var @event in events)
                {
                    using (var command = new SqlCommand(@"insert into [IsaPolicyEvents]
                                                          ([EventContextId], [EventId], [EventDateTime], [EventType], [Data]) 
                                                          values 
                                                          (@EventContextId, @EventId, @EventDateTime, @EventType, @Data)", connection) { CommandType = CommandType.Text })
                    {
                        var data = Serialize(@event);
                        command.Parameters.Add("@EventContextId", SqlDbType.UniqueIdentifier).Value = @event.EventContextId;
                        command.Parameters.Add("@EventId", SqlDbType.UniqueIdentifier).Value = @event.EventId;
                        command.Parameters.Add("@EventDateTime", SqlDbType.DateTime).Value = @event.EventDateTime;
                        command.Parameters.Add("@EventType", SqlDbType.NVarChar, 255).Value = @event.GetType().Name;
                        command.Parameters.Add("@Data", SqlDbType.NVarChar, -1).Value = data;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private static string Serialize(IsaPolicyEvent value)
        {
            return JsonConvert.SerializeObject(value, JsonSerializerSettings);
        }

        private static IsaPolicyEvent Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<IsaPolicyEvent>(value, JsonSerializerSettings);
        }
    }
}

//USE[PolicyAdminDemoDB]
//GO
///****** Object:  Table [dbo].[IsaPolicyEvents]    Script Date: 01/05/2017 21:50:30 ******/
//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO
//CREATE TABLE[dbo].[IsaPolicyEvents]
//(

//[SequenceId][bigint] IDENTITY(1,1) NOT NULL,

//[EventContextId] [uniqueidentifier]
//NOT NULL,

//[EventId] [uniqueidentifier]
//NOT NULL,

//[EventType] [nvarchar] (255) NOT NULL,

//[EventDateTime] [datetime]
//NOT NULL,

//[Data] [nvarchar] (max) NOT NULL,
//[jsonCustomerId]  AS(json_value([Data],'$.CustomerId')),
//[jsonPolicyNumber]  AS(json_value([Data],'$.PolicyNumber')),
//CONSTRAINT[PK_SinglePolicyEventStore] PRIMARY KEY NONCLUSTERED
//(
//[EventId] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
//) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]
//GO
//ALTER TABLE[dbo].[IsaPolicyEvents] ADD CONSTRAINT[DF_SinglePolicyEventStore_EventDateTime]  DEFAULT(getdate()) FOR[EventDateTime]
//GO