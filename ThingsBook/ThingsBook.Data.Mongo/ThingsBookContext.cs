using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Configuration;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    /// <summary>
    /// Mondo database context
    /// </summary>
    public class ThingsBookContext
    {
        private IMongoDatabase _database { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThingsBookContext"/> class.
        /// </summary>
        public ThingsBookContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);
            _database = client.GetDatabase(connection.DatabaseName);
        }

        /// <summary>
        /// Gets the historical lends collection.
        /// </summary>
        public IMongoCollection<HistoricalLend> History
        {
            get { return _database.GetCollection<HistoricalLend>("HistoricalLend"); }
        }

        /// <summary>
        /// Gets the users collection.
        /// </summary>
        public IMongoCollection<User> Users
        {
            get { return _database.GetCollection<User>("User"); }
        }

        /// <summary>
        /// Gets the things collection.
        /// </summary>
        public IMongoCollection<Thing> Things
        {
            get { return _database.GetCollection<Thing>("Things"); }
        }

        /// <summary>
        /// Gets the categories collection.
        /// </summary>
        public IMongoCollection<Category> Categories
        {
            get { return _database.GetCollection<Category>("Category"); }
        }

        /// <summary>
        /// Gets the friends collection.
        /// </summary>
        public IMongoCollection<Friend> Friends
        {
            get { return _database.GetCollection<Friend>("Friend"); }
        }

        /// <summary>
        /// Registers the class maps.
        /// </summary>
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
