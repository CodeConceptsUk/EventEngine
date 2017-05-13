CREATE TABLE
	dbo.Sequencing
	(
		[Type][nvarchar](255) NOT NULL,
		[Sequence] [bigint] NOT NULL,
		CONSTRAINT[PK_Sequencing] PRIMARY KEY CLUSTERED
		(
			[Type] ASC
		)
		WITH
		(
			PAD_INDEX = OFF,
			STATISTICS_NORECOMPUTE = OFF,
			IGNORE_DUP_KEY = OFF,
			ALLOW_ROW_LOCKS = ON,
			ALLOW_PAGE_LOCKS = ON
		) ON [PRIMARY]
	) ON [PRIMARY]
GO

ALTER TABLE[dbo].[Sequencing] ADD CONSTRAINT[DF_Sequencing_Seqence]  DEFAULT((0)) FOR[Sequence]
GO

  
CREATE PROCEDURE
	dbo.GetNextSequence
		@Type nvarchar(255)
	AS
	BEGIN
		SET NOCOUNT ON;

		DECLARE @ErrorMessage NVARCHAR(MAX),
				@ErrorSeverity int, 
				@ErrorState int;

		BEGIN TRANSACTION
			begin try
				update Sequencing
					set
						[Sequence] = [Sequence] + 1
					where
						[Type] = @Type;
				if (@@ROWCOUNT = 0)
					begin
						select @ErrorMessage = 'There is not sequence for ''' + @Type + ''''
						raiserror(@ErrorMessage, 18, -1);
					end
				select[Sequence] from[Sequencing] s where[Type] = @Type;
			end try
			begin catch
				select @ErrorMessage = ERROR_MESSAGE() + ' Line ' + cast(ERROR_LINE() as nvarchar(5)), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
				rollback transaction
				raiserror(@ErrorMessage, @ErrorSeverity, @ErrorState);
				return 0;
			end catch
		commit transaction
	END
GO