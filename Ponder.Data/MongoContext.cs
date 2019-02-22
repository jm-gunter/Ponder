using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
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
            _collection = db.GetCollection<T>(nameof(T));
        }

        #region CREATE
        public async Task CreateAsync(T document) => await CreateAsync(new List<T> { document });

        public async Task CreateAsync(IEnumerable<T> documents)
        {
            // TODO: validate document
            await _collection.InsertManyAsync(documents);
        }
        #endregion

        #region READ
        public async Task<T> ReadAsync()
        {
            throw new NotImplementedException();

            //return await _collection.FindAsync(filter, options, cancellationToken);
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
