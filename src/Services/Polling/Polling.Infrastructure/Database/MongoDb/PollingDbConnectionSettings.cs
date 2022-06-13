
namespace Polling.Infrastructure.Database.MongoDb
{
    public class PollingDbConnectionSettings
    {

        public string Port { get; set; }
        public string Host { get; set; } 
        public string User { get; set; } 
        public string Password { get; set; } 

        public string ConnectionString
        {
            get
            {
                return $"mongodb+srv://{User}:{Password}@{Host}";
                //return $"mongodb+srv://{User}:{Password}@{Host}:{Port}";
            }

        }

        public string DatabaseName { get; set; } 
        public string PollingCollectionName { get; set; } 

    }
}
