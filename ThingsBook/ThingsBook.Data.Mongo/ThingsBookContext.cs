using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using ThingsBook.Data.Interface;

namespace ThingsBook.Data.Mongo
{
    public class ThingsBookContext
    {
        public IMongoDatabase Database { get; }

        public ThingsBookContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);
            Database = client.GetDatabase(connection.DatabaseName);
        }

        public IMongoCollection<HistoricalLend> History
        {
            get { return Database.GetCollection<HistoricalLend>("HistoricalLend"); }
        }

        public IMongoCollection<User> Users
        {
            get { return Database.GetCollection<User>("User"); }
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
                cm.GetMemberMap(c => c.Categories).SetDefaultValue(new List<Category>());
                cm.GetMemberMap(c => c.Friends).SetDefaultValue(new List<Friend>());
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
