using AutoMapper;
using System;

namespace ThingsBook.BusinessLogic
{
    public static class ModelsConverter
    {
        public static Data.Interface.Category ToDataModel(Models.Category category, Guid userId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Models.Category, Data.Interface.Category>()).CreateMapper();
            var result = mapper.Map<Data.Interface.Category>(category);
            result.UserId = userId;
            return result;
        }

        public static Models.Category ToBLModel(Data.Interface.Category category)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.Category, Models.Category>()).CreateMapper();
            return mapper.Map<Models.Category>(category);
        }

        public static Data.Interface.Thing ToDataModel(Models.Thing thing, Guid userId)
        {
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Models.Thing, Data.Interface.Thing>()
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

        public static Models.Thing ToBLModel(Data.Interface.Thing thing)
        {
            var mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Data.Interface.Thing, Models.Thing>()
                .ForMember(
                    destination => destination.Lend,
                    opts => opts.MapFrom(source => ToBLModel(source.Lend))
                    )
                )
                .CreateMapper();
            return mapper.Map<Models.Thing>(thing);
        }

        public static Data.Interface.Friend ToDataModel(Models.Friend friend, Guid userId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Models.Friend, Data.Interface.Friend>()).CreateMapper();
            var result = mapper.Map<Data.Interface.Friend>(friend);
            result.UserId = userId;
            return result;
        }

        public static Models.Friend ToBLModel(Data.Interface.Friend friend)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.Friend, Models.Friend>()).CreateMapper();
            return mapper.Map<Models.Friend>(friend);
        }

        public static Data.Interface.Lend ToDataModel(Models.Lend lend)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Models.Lend, Data.Interface.Lend>()).CreateMapper();
            return mapper.Map<Data.Interface.Lend>(lend);
        }

        public static Models.Lend ToBLModel(Data.Interface.Lend lend)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.Lend, Models.Lend>()).CreateMapper();
            return mapper.Map<Models.Lend>(lend);
        }

        public static Data.Interface.User ToDataModel(Models.User user)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Models.User, Data.Interface.User>()).CreateMapper();
            return mapper.Map<Data.Interface.User>(user);
        }

        public static Models.User ToBLModel(Data.Interface.User user)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.User, Models.User>()).CreateMapper();
            return mapper.Map<Models.User>(user);
        }
    }
}
