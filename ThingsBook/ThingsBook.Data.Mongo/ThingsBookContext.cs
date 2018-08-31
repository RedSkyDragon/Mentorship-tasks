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
        public ThingsBookContext() : this(ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString) { }

        public ThingsBookContext(string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);
            _database = client.GetDatabase(connection.DatabaseName);
        }

        static ThingsBookContext()
        {
            RegisterClassMaps();
        }

        /// <summary>
        /// Gets the historical lends collection.
        /// </summary>
        public IMongoCollection<HistoricalLend> History
        {
            get { return Collection<HistoricalLend>(); }
        }

        /// <summary>
        /// Gets the users collection.
        /// </summary>
        public IMongoCollection<User> Users
        {
            get { return Collection<User>(); }
        }

        /// <summary>
        /// Gets the things collection.
        /// </summary>
        public IMongoCollection<Thing> Things
        {
            get { return Collection<Thing>(); }
        }

        /// <summary>
        /// Gets the categories collection.
        /// </summary>
        public IMongoCollection<Category> Categories
        {
            get { return Collection<Category>(); }
        }

        /// <summary>
        /// Gets the friends collection.
        /// </summary>
        public IMongoCollection<Friend> Friends
        {
            get { return Collection<Friend>(); }
        }

        private IMongoCollection<T> Collection<T>() where T: Entity
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }

        /// <summary>
        /// Registers the class maps.
        /// </summary>
        public static void RegisterClassMaps()
        {
            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
            conventionPack.Add(new IgnoreIfNullConvention(true));
            ConventionRegistry.Register("conventions", conventionPack, t => true);
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
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
            BsonClassMap.RegisterClassMap<Lend>(cm =>
            {
                cm.AutoMap();
                cm.GetMemberMap(c => c.LendDate).SetSerializer(new DateTimeSerializer(dateOnly: true));
                cm.GetMemberMap(c => c.LendDate).SetSerializer(new DateTimeSerializer(kind: DateTimeKind.Local));
            });
            BsonClassMap.RegisterClassMap<Thing>(cm =>
            {
                cm.AutoMap();
                cm.GetMemberMap(c => c.UserId).SetIsRequired(true);               
            });
        }
    }
}
