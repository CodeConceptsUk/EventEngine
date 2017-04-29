CREATE TABLE
	dbo.Events
	(
		Id uniqueidentifier not null,
		ContextId uniqueidentifier not null,
		TypeId uniqueidentifier not null,
		CommandId uniqueidentifier not null,
		Instant datetime2 not null,
		Creator nvarchar(250) not null,
		[Data] nvarchar(MAX) not null,
		CONSTRAINT PK_Events_Id PRIMARY KEY NONCLUSTERED (Id)
	)
;

ALTER TABLE dbo.Events ADD CONSTRAINT DF_Events_Instant DEFAULT GETUTCDATE() FOR Instant;

CREATE CLUSTERED INDEX IX_Events_ContextId_Instant ON dbo.Events (ContextId, Instant DESC);