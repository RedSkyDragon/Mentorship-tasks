using System;
using System.Threading.Tasks;

namespace ThingsBook.Data.Interface
{
    public interface ILends
    {
        Task<Lend> GetLend(Guid thingId);

        Task UpdateLend(Guid thingId, Lend lend);

        Task DeleteLend(Guid thingId);

        Task CreateLend(Guid thingId, Lend lend);
    }
}
