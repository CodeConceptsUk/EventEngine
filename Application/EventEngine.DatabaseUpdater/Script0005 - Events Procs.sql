CREATE PROC InsertEvent
    @ContextId UNIQUEIDENTIFIER,
    @Instant DATETIME2,
    @Data NVARCHAR(MAX)
AS
    SET NOCOUNT ON
	DECLARE @NonGuid uniqueidentifier = '00000000-0000-0000-0000-000000000000'
    INSERT INTO
        Events
            (Id, ContextId, TypeId, CommandId, Instant, Creator, [Data])
    VALUES
        (NEWID(), @ContextId, @NonGuid, @NonGuid, SYSDATETIME(), 'Test', @Data);
GO

CREATE PROC SelectEventsInContext
    @ContextId UNIQUEIDENTIFIER
AS
    SET NOCOUNT ON
    SELECT 
		Id,
		ContextId,
		Instant,
		[Data]
    FROM
        Events
    WHERE
        ContextId = @ContextId
    ORDER BY
        [Instant]
    ASC;
GO
