using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PaymentService.Application.Contracts.Persistence.Repositories.Commons;
using PaymentService.Application.Settings;
using PaymentService.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PaymentService.Infrastructure.Contracts.Persistence.Commons
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly AppMongoDbContext _dbContext;
        private readonly IMongoCollection<T> _collection;

        public RepositoryBase(IOptions<MongoSettings> settings)
        {
            _dbContext = new AppMongoDbContext(settings);
            _collection = _dbContext.GetCollection<T>();
        }

        public void Add(T entity)
        {
            _collection.InsertOne(entity);
        }

        public IReadOnlyList<T> GetAll()
        {
            return _collection.AsQueryable().ToList();
        }

        public T GetById(string Id)
        {
            var objectId = ObjectId.Parse(Id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            return _collection.Find(filter).FirstOrDefault();
        }

        public T GetFilterBy(Expression<Func<T, bool>> predicate)
        {
            return _collection.Find(predicate).FirstOrDefault();
        }

        public void Remove(string Id)
        {
            var objectId = ObjectId.Parse(Id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            _collection.FindOneAndDelete(filter);
        }

        public void Update(T entity, string Id)
        {
            var objectId = ObjectId.Parse(Id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            _collection.ReplaceOne(filter, entity);
        }
    }
}
