using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThingsBook.DataAccessInterface;

namespace DataAccessImplement
{
    public class ThingsBookContext
    {
        private IMongoDatabase _database;

        public ThingsBookContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);
            _database = client.GetDatabase(connection.DatabaseName);
        }

        public IMongoCollection<HistoricalLend> History
        {
            get { return _database.GetCollection<HistoricalLend>("HistoricalLend"); }
        }

        public IMongoCollection<User> Users
        {
            get { return _database.GetCollection<User>("User"); }
        }

        public static void RegisterClassMaps()
        {
            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
            BsonSerializer.RegisterIdGenerator(typeof(Guid), GuidGenerator.Instance);
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.GetMemberMap(c => c.Name).SetIsRequired(true);
            });
            BsonClassMap.RegisterClassMap<HistoricalLend>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.LendDate).SetSerializer(new DateTimeSerializer(dateOnly: true));
                cm.MapMember(c => c.LendDate).SetSerializer(new DateTimeSerializer(kind: DateTimeKind.Local));
                cm.MapMember(c => c.ReturnDate).SetSerializer(new DateTimeSerializer(dateOnly: true));
                cm.MapMember(c => c.ReturnDate).SetSerializer(new DateTimeSerializer(kind: DateTimeKind.Local));
            });
        }
    }
}
