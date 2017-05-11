CREATE TABLE
	dbo.PolicyViewSnapshots
	(
		[ContextId] UNIQUEIDENTIFIER NOT NULL,
		[DateTime] DATETIME2 NOT NULL,
		[Data] NVARCHAR(MAX) NOT NULL,
		CONSTRAINT PK_PolicyViewSnapshots_DateTime PRIMARY KEY CLUSTERED ([ContextId], [DateTime])
	)
;

