namespace Backend.SQL
{
    public class DefaultConnection
    {
        public string ConnectionString { get; set; }
        public DefaultConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
