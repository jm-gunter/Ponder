using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Ponder.Common;
using Ponder.Data;
using Ponder.Models;
using Xunit;

namespace Ponder.Tests
{
    /// <summary>
    /// XUnit tests for MongoContext class
    /// </summary>
    public class MongoContextTests : IDisposable
    {
        private const string connection = @"mongodb://localhost:27017";
        private const string db = "testDB";
        private readonly MongoClient _client;
        private readonly Game _tGame = new Game() { Name = "Test Game", Date = DateTime.Now };
        private readonly TestConfiguration _config;

        public MongoContextTests()
        {
            _client = new MongoClient($"{connection}/{db}");
            _config = new TestConfiguration();
            _config[ConfigKeys.ConnectionString] = connection;
            _config[ConfigKeys.MongoDatabase] = db;
        }

        public void Dispose()
        {
            _client.DropDatabase(db);
        }

        [Fact]
        public async Task CreateAsync_ExpectOk()
        {
            var context = new MongoContext<Game>(_config);
            await context.CreateAsync(_tGame); // this will exercise both overloads of CreateAsync

            var doc = _client.GetDatabase(db)
                             .GetCollection<Game>(typeof(Game).Name)
                             .Find(new ObjectFilterDefinition<Game>(_tGame))
                             .Single();

            Assert.Equal(_tGame._id, doc._id);
        }

        [Fact]
        public async Task ReadAsync_ExpectOk()
        {
            var context = new MongoContext<Game>(_config);
            _client.GetDatabase(db)
                   .GetCollection<Game>(typeof(Game).Name)
                   .InsertOne(_tGame);

            var result = await context.ReadAsync();

            Assert.Contains(result, g => g._id == _tGame._id);
        }

        [Fact]
        public async Task ReadAsyncWithFilter_ExpectOk()
        {
            var context = new MongoContext<Game>(_config);
            _client.GetDatabase(db)
                   .GetCollection<Game>(typeof(Game).Name)
                   .InsertOne(_tGame);

            var result = await context.ReadAsync("{ Name: \"Test Game\"}");

            Assert.Contains(result, g => g._id == _tGame._id);
        }
    }
}
