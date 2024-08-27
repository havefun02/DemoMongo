using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;

namespace App
{
    public class ServiceDemo:IServiceDemo
    {
        private readonly IMongoCollection<User> _collection;

        public ServiceDemo(IMongoDatabase database)
        {
            _collection = database.GetCollection<User>("myCollection");
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task InsertDocumentAsync(User document)
        {
            document.Id = Guid.NewGuid().ToString();
            await _collection.InsertOneAsync(document);
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }
        public async Task ReplaceDocumentAsync(User user)
        {
            await _collection.ReplaceOneAsync(e => e.Id == user.Id, user);
        }

        public async Task DeleteDocumentAsync(string id)
        {
            await _collection.DeleteOneAsync(e => e.Id == id);
        }
    }

}
