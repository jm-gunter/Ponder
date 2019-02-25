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

        #region CREATE tests
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
        #endregion

        #region READ tests
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

            var result = await context.ReadAsync(g => g.Name == _tGame.Name);

            Assert.Contains(result, g => g._id == _tGame._id);
        }
        #endregion

        #region UPDATE tests
        [Fact]
        public async Task UpdateAsync_ExpectOk()
        {
            var context = new MongoContext<Game>(_config);
            _client.GetDatabase(db)
                   .GetCollection<Game>(typeof(Game).Name)
                   .InsertOne(_tGame);
            var newGame = new Game()
            {
                Name = "New Game",
            };

            // Update _tGame to newGame
            await context.UpdateAsync(g => g.Name == _tGame.Name, newGame);

            var result = _client.GetDatabase(db)
                                .GetCollection<Game>(typeof(Game).Name)
                                .AsQueryable();

            Assert.Contains(result, g => g.Name == newGame.Name);
        }
        #endregion

        #region DELETE tests
        [Fact]
        public async Task DeleteAsync_WithFilter_ExpectOk()
        {
            var context = new MongoContext<Game>(_config);
            _client.GetDatabase(db)
                   .GetCollection<Game>(typeof(Game).Name)
                   .InsertOne(_tGame);

            // Update _tGame to newGame
            await context.DeleteAsync(g => g.Name == _tGame.Name);

            var result = _client.GetDatabase(db)
                                .GetCollection<Game>(typeof(Game).Name)
                                .AsQueryable();

            Assert.DoesNotContain(result, g => g.Name == _tGame.Name);
        }
        #endregion
    }
}
