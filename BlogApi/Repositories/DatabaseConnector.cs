namespace BlogApi.Repositories
{
    using System.Data.SqlClient;

    public static class DatabaseConnector
    {
        private static string datasource = ".";
        //private static string userId = "";
        //private static string pw = "";
        private static string initialCatalog = "Blog";
        private static int connectTimeout = 60;

        public static SqlConnectionStringBuilder getDbConnectionStringBuilder()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = datasource;
            builder.InitialCatalog = initialCatalog;
            builder.ConnectTimeout = connectTimeout;
            builder.IntegratedSecurity = true;
            //builder.UserID = userId;
            //builder.Password = pw;

            return builder;
        }
    }
}