using Song_Project.Models;
using System.Data.SqlClient;

public class ApplicationManager
{
    public String GetConnectionString()
    {
        String connectionString = "";

        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = "localhost";
        builder["Trusted_Connection"] = "true";
        builder.InitialCatalog = "Music";

        connectionString = builder.ConnectionString;

        return connectionString;
    }

    private static ApplicationManager instance = null;

    public static ApplicationManager Instance {
        get {
            if(instance == null) {
                instance = new ApplicationManager();
            }

            return instance;
        }
    }
}