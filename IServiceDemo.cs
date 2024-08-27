using MongoDB.Bson;
using MongoDB.Driver;

namespace App
{
    public interface IServiceDemo
    {
        Task InsertDocumentAsync(User user);
        Task ReplaceDocumentAsync(User user);
        Task DeleteDocumentAsync(string id);
        Task<List<User>> GetAllAsync();
         Task<User> GetByIdAsync(string id);


    }
}
