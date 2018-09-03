using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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
        /// Initializes the <see cref="ThingsBookContext"/> class.
        /// </summary>
        static ThingsBookContext()
        {
            RegisterClassMaps();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThingsBookContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="client">The client.</param>
        public ThingsBookContext(string connectionString, IMongoClient client)
        {
            var connection = new MongoUrlBuilder(connectionString);
            _database = client.GetDatabase(connection.DatabaseName);
            CreateIndexes();
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

        private static void RegisterClassMaps()
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

        private IMongoCollection<T> Collection<T>() where T : Entity
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }

        private void CreateIndexes()
        {
            var options = new CreateIndexOptions { Background = true };
            Things.Indexes.CreateMany(new List<CreateIndexModel<Thing>>()
            {
                new CreateIndexModel<Thing>(Builders<Thing>.IndexKeys.Ascending(t => t.CategoryId), options),
                new CreateIndexModel<Thing>(Builders<Thing>.IndexKeys.Ascending(t => t.UserId), options)
            });
            Categories.Indexes.CreateOne(new CreateIndexModel<Category>(Builders<Category>.IndexKeys.Ascending(t => t.UserId), options));
            Friends.Indexes.CreateOne(new CreateIndexModel<Friend>(Builders<Friend>.IndexKeys.Ascending(t => t.UserId), options));
            History.Indexes.CreateMany(new List<CreateIndexModel<HistoricalLend>>()
            {
                new CreateIndexModel<HistoricalLend>(Builders<HistoricalLend>.IndexKeys.Ascending(h => h.UserId), options),
                new CreateIndexModel<HistoricalLend>(Builders<HistoricalLend>.IndexKeys.Ascending(h => h.FriendId), options),
                new CreateIndexModel<HistoricalLend>(Builders<HistoricalLend>.IndexKeys.Ascending(h => h.ThingId), options)
            });
        }

    }
}
