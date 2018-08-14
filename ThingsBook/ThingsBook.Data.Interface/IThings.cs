using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface IThings
    {
        Task<IEnumerable<Thing>> GetThings(Guid userId);

        Task<IEnumerable<Thing>> GetThings(Guid userId, Guid categoryId);

        Task<Thing> GetThing(Guid id);

        Task UpdateThing(Thing thing);

        Task DeleteThing(Guid id);

        Task CreateThing(Thing thing);
    }
}
