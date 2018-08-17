﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public interface IThingsBL
    {
        Task<Thing> CreateThing(Guid userId, Thing thing);

        Task<Thing> UpdateThing(Guid userId, Thing thing);

        Task DeleteThing(Guid userId, Guid id);

        Task<Thing> GetThing(Guid userId, Guid id);

        Task<IEnumerable<Thing>> GetThings(Guid userId);

        Task<IEnumerable<Thing>> GetThingsForCategory(Guid userId, Guid categoryId);

        Task<FilteredLends> GetThingLends(Guid userId, Guid thingId);

        Task<Category> CreateCategory(Guid userId, Category category);

        Task<Category> UpdateCategory(Guid userId, Category category);

        Task DeleteCategoryWithThings(Guid userId, Guid id);

        Task DeleteCategoryWithReplacement(Guid userId, Guid categoryId, Guid replacementId);

        Task<Category> GetCategory(Guid userId, Guid id);

        Task<IEnumerable<Category>> GetCategories(Guid userId);
    }
}
