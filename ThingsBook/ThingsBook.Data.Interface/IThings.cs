using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface IThings
    {
        Task<IEnumerable<Thing>> GetThings(Guid userId);

        Task<IEnumerable<Thing>> GetThingsForCategory(Guid userId, Guid categoryId);

        Task<Thing> GetThing(Guid userId, Guid id);

        Task<IEnumerable<Thing>> GetThingsForFriend(Guid userId, Guid friedId);

        Task UpdateThing(Guid userId, Thing thing);

        Task UpdateThingsCategory(Guid userId, Guid categoryId, Guid replacementId);

        Task DeleteThing(Guid userId, Guid id);

        Task DeleteThings(Guid userId);

        Task DeleteThingsForCategory(Guid userId, Guid categoryId);

        Task CreateThing(Guid userId, Thing thing);
    }
}
