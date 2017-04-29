CREATE TABLE
	dbo.Commands
	(
		Id uniqueidentifier not null,
		[Version] int not null,
		[Name] nvarchar(250) not null,
		[Data] nvarchar(MAX) not null,
		CONSTRAINT PK_Commands_Id PRIMARY KEY NONCLUSTERED (Id)
	)
;

CREATE NONCLUSTERED INDEX IX_Commands_Version ON dbo.Commands ([Version]);

ALTER TABLE dbo.Events ADD CONSTRAINT FK_Events_CommandId_Commands_Id FOREIGN KEY (CommandId) REFERENCES dbo.Commands(Id);