using System;
using System.Collections.Generic;

namespace ThingsBook.Data.Interface
{
    public interface IThingsBL
    {
        IEnumerable<Thing> GetThings(Guid userId);

        IEnumerable<Thing> GetThings(Guid userId, Guid categoryId);

        Thing GetThing(Guid userId, Guid categoryId, Guid id);

        void UpdateThing(Guid userId, Guid categoryId, Thing thing);

        void DeleteThing(Guid userId, Guid categoryId, Guid id);

        void CreateThing(Guid userId, Guid categoryId, Thing thing);

        IEnumerable<Lend> GetLends(Guid userId);

        Lend GetLend(Guid userId, Guid categoryId, Guid thingId);

        void UpdateLend(Guid userId, Guid categoryId, Guid thingId, Lend lend);

        void DeleteLend(Guid userId, Guid categoryId, Guid thingId);

        void CreateLend(Guid userId, Guid categoryId, Guid thingId, Lend lend);
    }
}
