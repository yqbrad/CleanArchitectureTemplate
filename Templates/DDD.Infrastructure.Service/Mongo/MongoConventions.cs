using System.Collections.Generic;
using MongoDB.Bson.Serialization.Conventions;

namespace $safeprojectname$.Mongo
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