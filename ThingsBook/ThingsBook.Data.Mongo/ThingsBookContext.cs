using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Configuration;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    public class ThingsBookContext
    {
        private IMongoDatabase _database { get; }

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

        public IMongoCollection<Thing> Things
        {
            get { return _database.GetCollection<Thing>("Things"); }
        }

        public IMongoCollection<Category> Categories
        {
            get { return _database.GetCollection<Category>("Category"); }
        }

        public IMongoCollection<Friend> Friends
        {
            get { return _database.GetCollection<Friend>("Friend"); }
        }

        public static void RegisterClassMaps()
        {
            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
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
            BsonClassMap.RegisterClassMap<Friend>(cm =>
            {
                cm.AutoMap();
                cm.GetMemberMap(c => c.UserId).SetIsRequired(true);
            });
            BsonClassMap.RegisterClassMap<Category>(cm =>
            {
                cm.AutoMap();
                cm.GetMemberMap(c => c.UserId).SetIsRequired(true);
            });
            BsonClassMap.RegisterClassMap<Thing>(cm =>
            {
                cm.AutoMap();
                cm.GetMemberMap(c => c.UserId).SetIsRequired(true);
                cm.GetMemberMap(c => c.Lend.LendDate).SetSerializer(new DateTimeSerializer(dateOnly: true));
                cm.GetMemberMap(c => c.Lend.LendDate).SetSerializer(new DateTimeSerializer(kind: DateTimeKind.Local));
            });
        }
    }
}
