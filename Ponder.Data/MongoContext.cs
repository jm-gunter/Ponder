using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Ponder.Common;

namespace Ponder.Data
{
    /// <summary>
    /// Provides CRUD operations for Mongo DB using mongo csharp driver
    /// http://mongodb.github.io/mongo-csharp-driver/2.7/
    /// </summary>
    public class MongoContext<T> : IDataContext<T>
    {
        private readonly IMongoCollection<T> _collection;

        public MongoContext(IConfiguration config)
        {
            var client = new MongoClient(config[ConfigKeys.ConnectionString]);
            var db = client.GetDatabase(config[ConfigKeys.MongoDatabase]);
            _collection = db.GetCollection<T>(typeof(T).Name);
        }

        #region CREATE
        /// <summary>
        /// Inserts a single record
        /// </summary>
        public async Task CreateAsync(T document) => await CreateAsync(new List<T> { document });

        /// <summary>
        /// Inserts multiple records
        /// </summary>
        public async Task CreateAsync(IEnumerable<T> documents)
        {
            // TODO: validate document

            await _collection.InsertManyAsync(documents);
        }
        #endregion

        #region READ
        /// <summary>
        /// Returns all records in the collection.
        /// </summary>
        /// <returns>The async.</returns>
        public async Task<IEnumerable<T>> ReadAsync()
        {
            var filter = FilterDefinition<T>.Empty;

            var cursor = await _collection.FindAsync<T>(filter);
            return cursor.ToList();
        }

        /// <summary>
        /// Returns a list of records matching the specified filter
        /// </summary>
        /// <returns>IEnumerable<typeparamref name="T"/>></returns>
        /// <param name="filter">a Linq expression to filter by</param>
        public async Task<IEnumerable<T>> ReadAsync(Expression<Func<T, bool>> filter)
        {
            var cursor = await _collection.FindAsync<T>(filter);
            return cursor.ToList();
        }
        #endregion

        #region UPDATE
        public async Task<T> UpdateAsync(Expression<Func<T, bool>> filter, T obj)
        {
            var result = await _collection.ReplaceOneAsync(filter, obj);
            var bson = result.ToBsonDocument();
            return BsonSerializer.Deserialize<T>(bson);
        }
        #endregion

        #region DELETE
        public async Task DeleteAsync(Expression<Func<T, bool>> filter)
        {
            await _collection.DeleteManyAsync(filter);
        }
        #endregion
    }
}
