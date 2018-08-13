using System;
using System.Collections.Generic;

namespace ThingsBook.Data.Interface
{
    public interface IThings
    {
        IEnumerable<Thing> GetThings(Guid userId);

        IEnumerable<Thing> GetThings(Guid userId, Guid categoryId);

        Thing GetThing(Guid userId, Guid categoryId, Guid id);

        void UpdateThing(Guid userId, Guid categoryId, Thing thing);

        void DeleteThing(Guid userId, Guid categoryId, Guid id);

        void CreateThing(Guid userId, Guid categoryId, Thing thing);
    }
}
