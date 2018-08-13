using System;
using System.Collections.Generic;

namespace ThingsBook.Data.Interface
{
    public interface ILends
    {
        IEnumerable<Lend> GetLends(Guid userId);

        Lend GetLend(Guid userId, Guid categoryId, Guid thingId);

        void UpdateLend(Guid userId, Guid categoryId, Guid thingId, Lend lend);

        void DeleteLend(Guid userId, Guid categoryId, Guid thingId);

        void CreateLend(Guid userId, Guid categoryId, Guid thingId, Lend lend);
    }
}
