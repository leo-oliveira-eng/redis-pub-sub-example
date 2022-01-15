using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using RedisExample.VaccinationCard.Domain.Models;

namespace RedisExample.VaccinationCard.Data.Mappings
{
    public static class EntityMapping
    {
        public static void MapEntity()
        {
            BsonClassMap.RegisterClassMap<Entity>(cm =>
            {
                BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));

                cm.AutoMap();

                cm.MapIdMember(x => x.Id);
            });
        }
    }
}
