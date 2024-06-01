using Cabin_API.Models;
using MongoDB.Driver;

namespace Cabin_API.Services.DataServices
{
    public class PriceService
    {
        private readonly IMongoCollection<Price> _collection;

        public PriceService(IConfiguration configuration, MongoDbConnectionService connectionService)
        {
            string collection_name = configuration.GetSection("MongoDB:TablePrices").Get<string>();
            _collection = connectionService.Database.GetCollection<Price>(collection_name);
        }

        public async Task<Price> CreateAsync(Price price)
        {
            await _collection.InsertOneAsync(price);
            return price;
        }

        public async Task<Price> GetAsync()
        {
            var filter = Builders<Price>.Filter.Empty;
            var sort = Builders<Price>.Sort.Descending(p => p.CreatedAt);
            Price latestPrice = await _collection.Find(filter)
                                               .Sort(sort)
                                               .FirstOrDefaultAsync();
            return latestPrice;
        }
    }
}
