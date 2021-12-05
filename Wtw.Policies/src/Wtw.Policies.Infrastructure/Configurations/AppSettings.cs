namespace Wtw.Policies.Infrastructure.Configurations
{
    public class AppSettings
    {
        public const string Domain = "Policies";
        public string ConnectionString { get; set; } = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Wtw.Policies;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\\Wtw.Policies.mdf";
    }
}
