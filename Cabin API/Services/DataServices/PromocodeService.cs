using Cabin_API.Models;
using MongoDB.Driver;

namespace Cabin_API.Services.DataServices
{
    public class PromocodeService
    {
        private readonly IMongoCollection<Promocode> _collection;

        public PromocodeService(IConfiguration configuration, MongoDbConnectionService service)
        {
            string collection_name = configuration.GetSection("MongoDB:TablePromocodes").Get<string>();
            _collection = service.Database.GetCollection<Promocode>(collection_name);
        }

        public async Task<Promocode> CreateAsync(Promocode promocode)
        {
            await _collection.InsertOneAsync(promocode);
            return promocode;
        }

        public async Task<Promocode> GetByIdAsync(string id)
        {
            var filter = Builders<Promocode>.Filter.Eq(p => p.Id, id);
            Promocode result = await _collection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Promocode> GetByCodeAsync(string code)
        {
            var filter = Builders<Promocode>.Filter.Eq(p => p.Code, code);
            Promocode result = await _collection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<DeleteResult> DeleteByIdAsync(string id)
        {
            var filter = Builders<Promocode>.Filter.Eq(p => p.Id, id);
            DeleteResult result = await _collection.DeleteOneAsync(filter);
            return result;
        }

        public async Task<Promocode> UpdateAsync(Promocode promocode)
        {
            var filter = Builders<Promocode>.Filter.Eq(p => p.Id, promocode.Id);
            var options = new FindOneAndReplaceOptions<Promocode>()
            {
                ReturnDocument = ReturnDocument.After
            };
            Promocode result = await _collection.FindOneAndReplaceAsync(filter, promocode, options);

            return result;
        }
    }
}
