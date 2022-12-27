using BankTransferService.Core.Entities;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace BankTransferService.Repo.Dapper.Infrastructure
{
    public class Connectionfactory : IConnectionFactory
    {
        IOptions<ReadConfig> _con;
        private readonly string connectionString;
        private static string dbSchema = "dbo";
        public Connectionfactory(IOptions<ReadConfig> con)
        {
            _con = con;
            connectionString = _con.Value.DefaultConnection;
        }
        public IDbConnection GetConnection
        {
            get
            {
                var conn = new SqlConnection(connectionString); //factory.CreateConnection(); //
                try
                {
                    //var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                    conn.ConnectionString = connectionString;
                    conn.Open();
                    return conn;
                }
                finally
                {
                    conn.Close();
                }
            }
        }



        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        public static class StoredProcedures
        {
            //Stored Procedures for Tb_Applicant
            public static string uspRegisterApplicant = $"{dbSchema}.usp_RegisterApplicant";
            public static string uspUploadCV = $"{dbSchema}.usp_UploadCV";
            public static string uspDownloadCV = $"{dbSchema}.usp_DownloadCV";


            public static string uspGetCoursePrograms = $"{dbSchema}.usp_GetCoursePrograms";
            public static string uspGetCountryList = $"{dbSchema}.usp_GetCountryList";


            public static string uspGetStates = $"{dbSchema}.usp_GetStates ";

            public static string uspGetLGAByStateId = $"{dbSchema}.usp_GetLGAByStateId ";

            public static string uspGetAllApplicants = $"{dbSchema}.usp_GetAllApplicants ";
            public static string uspGetApplicantById = $"{dbSchema}.usp_GetApplicantById ";
            public static string uspGetApplicantByEmailAddress = $"{dbSchema}.usp_GetApplicantByEmailAddress ";


            public static string uspDownloadApplicants = $"{dbSchema}.usp_DownloadApplicants";

            public static string uspGetApplicantResume = $"{dbSchema}.usp_GetApplicantResume";
            public static string uspGetApplicantPassport = $"{dbSchema}.usp_GetApplicantPassport";

            public static string uspVerifyPayment = $"{dbSchema}.usp_VerifyPayment";
            public static string uspProcessPayment = $"{dbSchema}.usp_ProcessPayment";

            public static string uspGetAllTransactions = $"{dbSchema}.usp_GetAllTransactions";
            public static string uspGetTrainingAmount = $"{dbSchema}.usp_GetTrainingAmount";

            public static string uspUpdateTrainingAmount = $"{dbSchema}.usp_UpdateTrainingAmount";
            public static string uspAddTraining = $"{dbSchema}.usp_AddTraining";

            public static string uspGetApplicantStats = $"{dbSchema}.usp_GetApplicantStats";


        }
    }
}
