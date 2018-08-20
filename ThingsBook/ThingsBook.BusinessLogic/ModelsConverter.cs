using AutoMapper;
using System;

namespace ThingsBook.BusinessLogic
{
    /// <summary>
    /// Static methods for model convertation.
    /// </summary>
    public static class ModelsConverter
    {
        /// <summary>
        /// Converts the business logic model to the data model.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The data model.</returns>
        public static Data.Interface.Category ToDataModel(Models.Category category, Guid userId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Models.Category, Data.Interface.Category>()).CreateMapper();
            var result = mapper.Map<Data.Interface.Category>(category);
            result.UserId = userId;
            return result;
        }

        /// <summary>
        /// Converts the data model to the business logic model.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>The business logic model.</returns>
        public static Models.Category ToBLModel(Data.Interface.Category category)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.Category, Models.Category>()).CreateMapper();
            return mapper.Map<Models.Category>(category);
        }

        /// <summary>
        /// Converts the business logic model to the data model.
        /// </summary>
        /// <param name="thing">The thing.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The data model.</returns>
        public static Data.Interface.Thing ToDataModel(Models.ThingWithLend thing, Guid userId)
        {
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Models.ThingWithLend, Data.Interface.Thing>()
                .ForMember(
                    destination => destination.Lend,
                    opts => opts.MapFrom(source => ToDataModel(source.Lend))
                    )
                )
                .CreateMapper();
            var result = mapper.Map<Data.Interface.Thing>(thing);
            result.UserId = userId;
            return result;
        }

        /// <summary>
        /// Converts the data model to the business logic model.
        /// </summary>
        /// <param name="thing">The thing.</param>
        /// <returns>The business logic model.</returns>
        public static Models.ThingWithLend ToBLModel(Data.Interface.Thing thing)
        {
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Data.Interface.Thing, Models.ThingWithLend>()
                .ForMember(
                    destination => destination.Lend,
                    opts => opts.MapFrom(source => ToBLModel(source.Lend))
                    )
                )
                .CreateMapper();
            return mapper.Map<Models.ThingWithLend>(thing);
        }

        /// <summary>
        /// Converts the business logic model to the data model.
        /// </summary>
        /// <param name="friend">The friend.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The data model.</returns>
        public static Data.Interface.Friend ToDataModel(Models.Friend friend, Guid userId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Models.Friend, Data.Interface.Friend>()).CreateMapper();
            var result = mapper.Map<Data.Interface.Friend>(friend);
            result.UserId = userId;
            return result;
        }

        /// <summary>
        /// Converts the data model to the business logic model.
        /// </summary>
        /// <param name="friend">The friend.</param>
        /// <returns>The business logic model.</returns>
        public static Models.Friend ToBLModel(Data.Interface.Friend friend)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.Friend, Models.Friend>()).CreateMapper();
            return mapper.Map<Models.Friend>(friend);
        }

        /// <summary>
        /// Converts the business logic model to the data model.
        /// </summary>
        /// <param name="lend">The lend.</param>
        /// <returns>The data model.</returns>
        public static Data.Interface.Lend ToDataModel(Models.Lend lend)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Models.Lend, Data.Interface.Lend>()).CreateMapper();
            return mapper.Map<Data.Interface.Lend>(lend);
        }

        /// <summary>
        /// Converts the data model to the business logic model.
        /// </summary>
        /// <param name="lend">The lend.</param>
        /// <returns>The business logic model.</returns>
        public static Models.Lend ToBLModel(Data.Interface.Lend lend)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.Lend, Models.Lend>()).CreateMapper();
            return mapper.Map<Models.Lend>(lend);
        }

        /// <summary>
        /// Converts the business logic model to the data model.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The data model.</returns>
        public static Data.Interface.User ToDataModel(Models.User user)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Models.User, Data.Interface.User>()).CreateMapper();
            return mapper.Map<Data.Interface.User>(user);
        }

        /// <summary>
        /// Converts the data model to the business logic model.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The business logic model.</returns>
        public static Models.User ToBLModel(Data.Interface.User user)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.User, Models.User>()).CreateMapper();
            return mapper.Map<Models.User>(user);
        }
    }
}
