using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.DataAccess.Sql
{
   public class SequencingSqlStore : ISequencingRepository
    {
        private static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["Sequencing"].ConnectionString;

        public string Get(string type)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("GetNextSequence", connection) {CommandType = CommandType.StoredProcedure})
                {
                    command.Parameters.Add("@Type", SqlDbType.NVarChar, 255).Value = type;
                    return ((long)command.ExecuteScalar()).ToString();
                }
            }
        }
    }
}

//USE[PolicyAdminDemoDB]
//GO

///****** Object:  Table [dbo].[Sequencing]    Script Date: 06/05/2017 16:06:44 ******/
//SET ANSI_NULLS ON
//GO

//SET QUOTED_IDENTIFIER ON
//GO

//CREATE TABLE[dbo].[Sequencing]
//(

//[Type][nvarchar](255) NOT NULL,

//[Sequence] [bigint]
//NOT NULL,
//CONSTRAINT[PK_Sequencing] PRIMARY KEY CLUSTERED
//(
//[Type] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
//) ON[PRIMARY]
//GO

//ALTER TABLE[dbo].[Sequencing] ADD CONSTRAINT[DF_Sequencing_Seqence]  DEFAULT((0)) FOR[Sequence]
//GO

    
//CREATE PROCEDURE[dbo].[GetNextSequence]
//@Type nvarchar(255)
//AS
//BEGIN


//SET NOCOUNT ON;

//declare @ErrorMessage nvarchar(max), @ErrorSeverity int, @ErrorState int;

//begin transaction


//begin try
//update[Sequencing] set
//[Sequence] = [Sequence] + 1

//where
//[Type] = @Type

//if (@@ROWCOUNT = 0)
//begin
//select @ErrorMessage = 'There is not sequence for ''' + @Type + ''''

//raiserror(@ErrorMessage, 18, -1);
//end

//select[Sequence] from[Sequencing] s where[Type] = @Type

//end try
//begin catch
//select @ErrorMessage = ERROR_MESSAGE() + ' Line ' + cast(ERROR_LINE() as nvarchar(5)), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

//rollback transaction


//raiserror(@ErrorMessage, @ErrorSeverity, @ErrorState);
//return 0;
//end catch

//commit transaction

//END
//GO




