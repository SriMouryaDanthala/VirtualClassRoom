using Npgsql;
using VirtualClassRoom.DataTypes;
namespace VirtualClassRoom.DataBase
{
    public class DataBaseManager
    {
        private string connectionString = "Server=localhost;Port=5432;Database=VirtualClassRoom;User Id=postgres;Password=Ammananna@6696;";

        public DataBaseManager()
        {

        }

        public string getConnectionString()
        {
            return this.connectionString;
            /*using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            return connection;*/
        }
    }
}
