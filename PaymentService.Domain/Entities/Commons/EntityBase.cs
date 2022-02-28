using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaymentService.Domain.Entities.Commons
{
    public abstract class EntityBase
    {
        [BsonId]
        public ObjectId ID { get; set; }
    }
}
