using System.Collections.Generic;
using MongoDB.Bson.Serialization.Conventions;

namespace DDD.Infrastructure.Service.Mongo
{
    public class MongoConventions : IConventionPack
    {
        public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String)
            };
    }
}