CREATE TABLE
	dbo.IsaPolicyEvents
	(
		[SequenceId] [bigint] IDENTITY(1,1) NOT NULL,
		[EventContextId] [uniqueidentifier] NOT NULL,
		[EventType] [nvarchar] (255) NOT NULL,
		[EventDateTime] [datetime] NOT NULL,
		[Data] [nvarchar] (max) NOT NULL,
		[jsonCustomerId]  AS(json_value([Data],'$.CustomerId')),
		[jsonPolicyNumber]  AS(json_value([Data],'$.PolicyNumber')),
		CONSTRAINT[PK_SinglePolicyEventStore] PRIMARY KEY NONCLUSTERED
		(
			[EventId] ASC
		)WITH(
			PAD_INDEX = OFF, 
			STATISTICS_NORECOMPUTE = OFF, 
			IGNORE_DUP_KEY = OFF, 
			ALLOW_ROW_LOCKS = ON, 
			ALLOW_PAGE_LOCKS = ON
		) ON [PRIMARY]
	) ON [PRIMARY]
	TEXTIMAGE_ON [PRIMARY]
;

CREATE CLUSTERED INDEX IX_IsaPolicyEvents_EventContextId_SequenceId ON dbo.IsaPolicyEvents (EventContextId, SequenceId DESC);

CREATE NONCLUSTERED INDEX IX_IsaPolicyEvents_EventType ON dbo.IsaPolicyEvents ([EventType] DESC);

ALTER TABLE[dbo].[IsaPolicyEvents] ADD CONSTRAINT [DF_IsaPolicyEvents_EventDateTime]  DEFAULT(getdate()) FOR [EventDateTime]
GO