using MongoDB.Bson;
using MongoDB.Driver;
namespace App {
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            //var mongoClient = host.Services.GetRequiredService<IMongoClient>();
            //var collection = mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection");

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }

}