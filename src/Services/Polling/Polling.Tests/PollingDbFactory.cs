using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Polling.Infrastructure.Database.MongoDb;
using Polling.Infrastructure.Repositories;
using Xunit;

namespace Polling.Tests
{
    public class PollingDbFactory
    {
        private Mock<IOptions<PollingDbConnectionSettings>> _mockOptions;

        private Mock<IMongoDatabase> _mockDB;

        private Mock<IMongoClient> _mockClient;

        public PollingDbFactory()
        {
            _mockOptions = new Mock<IOptions<PollingDbConnectionSettings>>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();

        }
        


        [Fact]
        public void MongoBookDBContext_Constructor_Success()
        {



        }



    }



}
