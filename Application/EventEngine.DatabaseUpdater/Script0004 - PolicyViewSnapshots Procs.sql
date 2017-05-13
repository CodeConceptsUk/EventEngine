
CREATE PROC InsertPolicyViewSnapshot
    @ContextId UNIQUEIDENTIFIER,
    @DateTime DATETIME2,
    @Data NVARCHAR(MAX)
AS
    SET NOCOUNT ON
    INSERT INTO
        PolicyViewSnapshots
            (ContextId, [DateTime], [Data])
    VALUES
        (@ContextId, @DateTime, @Data);
GO

CREATE PROC SelectLatestPolicyViewSnapshot
    @ContextId UNIQUEIDENTIFIER
AS
    SET NOCOUNT ON
    SELECT TOP 1
        [Data]
    FROM
        PolicyViewSnapshots
    WHERE
        ContextId = @ContextId
    ORDER BY
        [DateTime]
    DESC;
GO
