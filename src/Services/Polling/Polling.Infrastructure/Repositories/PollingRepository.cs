using MongoDB.Bson;
using MongoDB.Driver;
using Polling.Application.Contracts;
using Polling.Domain.Entities;
using Polling.Infrastructure.Database.MongoDb;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polling.Infrastructure.Repositories
{
    public class PollingRepository : IPollingRepository
    {

        private readonly IMongoCollection<EntityPosition> _entitiesCollection;
        private readonly FilterDefinitionBuilder<EntityPosition> _filterBuilder = Builders<EntityPosition>.Filter;


        public PollingRepository(PollingDbConnectionSettings dbCredentials)
        {
            var mongoClient = new MongoClient(dbCredentials.ConnectionString);
            var db = mongoClient.GetDatabase(dbCredentials.DatabaseName);
            _entitiesCollection = db.GetCollection<EntityPosition>(dbCredentials.PollingCollectionName);
        }


        public async Task CreatePositionAsync(EntityPosition newPosition)
        {
            var filter = _filterBuilder.Eq(position => position.EntityId, newPosition.EntityId)
                         & _filterBuilder.Eq(position => position.MeetingId, newPosition.MeetingId);

            var existingPosition = await _entitiesCollection.Find(filter).FirstOrDefaultAsync();

            if(existingPosition is null)
            {
                await _entitiesCollection.InsertOneAsync(newPosition);
            }
            
        }


        public async Task DeletePositionAsync(EntityPosition entityPosition)
        {
            var filter = _filterBuilder.Eq(position => position.Id, entityPosition.Id);
            await _entitiesCollection.DeleteOneAsync(filter);

        }


        public async Task<EntityPosition> GetPositionByIdAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(position => position.EntityId, id);
            return await _entitiesCollection.Find(filter).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<EntityPosition>> GetPositionsByMeetingIdAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(position => position.MeetingId, id);
            var positions = await _entitiesCollection.Find(filter).ToListAsync();

            return positions.Count == 0 ? null : positions;
        }


        public async Task<IEnumerable<EntityPosition>> GetPositionsByMeetingAndCreatorIdAsync(Guid userId, Guid meetingId)
        {
            var filter = _filterBuilder.Eq(position => position.MeetingId, meetingId) 
                        & _filterBuilder.Eq(position => position.CreatorId, userId);
            var positions = await _entitiesCollection.Find(filter).ToListAsync();

            return positions.Count == 0 ? null : positions;
        }

        public async Task UpdateWeightsAsync(IEnumerable<EntityPosition> positions)
        {

            foreach (var position in positions)
            {
                var filter = _filterBuilder.Eq(position => position.Id, position.Id);
                await _entitiesCollection.ReplaceOneAsync(filter, position);
            }
        }
    }
}
