using MongoDB.Driver;
using Cabin_API.Models;

namespace Cabin_API.Services.DataServices
{
    public class CabinService
    {
        private readonly IMongoCollection<Cabin> _collection;

        public CabinService(IConfiguration configuration, MongoDbConnectionService connectionService)
        {
            string collection_name = configuration.GetSection("MongoDB:TableCabins").Get<string>();
            _collection = connectionService.Database.GetCollection<Cabin>(collection_name);
        }

        public async Task<Cabin> CreateAsync(Cabin model)
        {
            await _collection.InsertOneAsync(model);
            return model;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var filter = Builders<Cabin>.Filter.Eq("Id", id);
            Cabin model = await _collection.FindOneAndDeleteAsync(filter);
            return model != null;
        }

        public async Task<Cabin> GetByIdAsync(int? id = 1)
        {
            var filter = Builders<Cabin>.Filter.Eq("Id", id);
            Cabin model = await _collection.Find(filter).FirstOrDefaultAsync();
            return model;
        }

        public async Task<List<Cabin>> GetAsync()
        {
            List<Cabin> result = await _collection.Find(_ => true).ToListAsync();
            return result;
        }

        public async Task<Cabin> PutAsync(Cabin model)
        {
            var filter = Builders<Cabin>.Filter.Eq("Id", model.Id);
            var options = new FindOneAndReplaceOptions<Cabin>()
            {
                ReturnDocument = ReturnDocument.After
            };
            model = await _collection.FindOneAndReplaceAsync(filter, model, options);

            return model;
        }

    }
}
