CREATE TABLE
	dbo.PolicyViewSnapshots
	(
		[DateTime] datetime2 not null,
		[Data] nvarchar(MAX) not null,
		CONSTRAINT PK_PolicyViewSnapshots_DateTime PRIMARY KEY CLUSTERED ([DateTime])
	)
;

