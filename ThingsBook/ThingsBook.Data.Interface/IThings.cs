using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface IThings
    {
        Task<IEnumerable<Thing>> GetThings(Guid userId);

        Task<IEnumerable<Thing>> GetThingsForCategory(Guid categoryId);

        Task<Thing> GetThing(Guid id);

        Task<Thing> GetThingForLend(Guid lendId);

        Task UpdateThing(Thing thing);

        Task DeleteThing(Guid id);

        Task DeleteThings(Guid userId);

        Task DeleteThingsForCategory(Guid categoryId);

        Task CreateThing(Thing thing);
    }
}
