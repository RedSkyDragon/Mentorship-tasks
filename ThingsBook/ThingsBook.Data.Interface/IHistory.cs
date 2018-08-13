using System;
using System.Collections.Generic;

namespace ThingsBook.Data.Interface
{
    public interface IHistoryBL
    {
        IEnumerable<HistoricalLend> GetHistLends(Guid userId);

        HistoricalLend GetHistLend(Guid id);

        void UpdateHistLend(HistoricalLend lend);

        void DeleteHistLend(Guid id);

        void CreateHistLend(HistoricalLend lend);

    }
}
