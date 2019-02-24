using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Ponder.Common;

namespace Ponder.Data
{
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
        /// <param name="filter">a valid MongoDB query string</param>
        public async Task<IEnumerable<T>> ReadAsync(string filter)
        {
            var cursor = await _collection.FindAsync(filter);
            return cursor.ToList();
        }
        #endregion

        #region UPDATE
        public async Task<T> UpdateAsync()
        {
            throw new NotImplementedException();

            //return await _collection.UpdateOneAsync(filter, update, options, cancellationToken);
        }
        #endregion

        #region DELETE
        public async Task<T> DeleteAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
