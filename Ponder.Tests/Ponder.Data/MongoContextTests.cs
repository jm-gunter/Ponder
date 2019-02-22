using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Ponder.Common;
using Ponder.Data;
using Ponder.Models;
using Xunit;

namespace Ponder.Tests
{
    public class MongoContextTests : IDisposable
    {
        private const string connection = @"mongodb://localhost:27017";
        private const string db = "testDB";
        private MongoClient _client;

        public MongoContextTests()
        {
            _client = new MongoClient($"{connection}/{db}");
        }

        public void Dispose()
        {
            _client.DropDatabase(db);
        }

        [Fact]
        public void Test1()
        {
            var config = new TestConfiguration();
            config[ConfigKeys.ConnectionString] = connection;
            config[ConfigKeys.MongoDatabase] = db;

            var context = new MongoContext<Game>(config);
            var game = new Game() { Name = "Test Game", Date = DateTime.Now };
            context.CreateAsync(game).RunSynchronously();

            var doc = _client.GetDatabase(db)
                             .GetCollection<Game>(nameof(Game))
                             .Find(new ObjectFilterDefinition<Game>(game));

            Console.WriteLine(doc);
        }
    }
}
