using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThingsBook.BusinessLogic.Models;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    /// <summary>
    /// Implementation of IFriendsBL interface.
    /// </summary>
    /// <seealso cref="ThingsBook.BusinessLogic.BaseBL" />
    /// <seealso cref="ThingsBook.BusinessLogic.IFriendsBL" />
    public class FriendsBL : BaseBL, IFriendsBL
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendsBL"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public FriendsBL(CommonDAL data): base(data) { }

        /// <summary>
        /// Creates the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friend">The friend.</param>
        /// <returns>
        /// Created friend.
        /// </returns>
        public async Task<Friend> Create(Guid userId, Friend friend)
        {
            await Data.Friends.CreateFriend(userId, friend);
            return await Data.Friends.GetFriend(userId, friend.Id);
        }

        /// <summary>
        /// Deletes the friend by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The friend identifier.</param>
        /// <returns></returns>
        public async Task Delete(Guid userId, Guid id)
        {
            await Data.Lends.DeleteFriendLends(userId, id);
            await Data.History.DeleteFriendHistory(userId, id);
            await Data.Friends.DeleteFriend(userId, id);
        }

        /// <summary>
        /// Gets all friends for user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List if friends.
        /// </returns>
        public Task<IEnumerable<Friend>> GetAll(Guid userId)
        {
            return Data.Friends.GetFriends(userId);
        }

        /// <summary>
        /// Gets the friend lends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>
        /// Filtered lends with active and history records.
        /// </returns>
        public async Task<FilteredLends> GetFriendLends(Guid userId, Guid friendId)
        {
            var friend = await Data.Friends.GetFriend(userId, friendId);
            var active = GetActiveLends(userId, friend);
            var history = GetHistoryLends(userId, friend);
            await Task.WhenAll(active, history);
            return new FilteredLends
            {
                ActiveLends = active.Result,
                History = history.Result
            };
        }

        /// <summary>
        /// Gets the friend by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The friend identifier.</param>
        /// <returns>
        /// Requested friend.
        /// </returns>
        public Task<Friend> GetOne(Guid userId, Guid id)
        {
            return Data.Friends.GetFriend(userId, id);
        }

        /// <summary>
        /// Updates the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friend">The friend.</param>
        /// <returns>
        /// Updated friend.
        /// </returns>
        public async Task<Friend> Update(Guid userId, Friend friend)
        {
            await Data.Friends.UpdateFriend(userId, friend);
            return await Data.Friends.GetFriend(userId, friend.Id);
        }

        /// <summary>
        /// Gets the history lends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friend">The friend.</param>
        /// <returns></returns>
        private async Task<IEnumerable<HistLend>> GetHistoryLends(Guid userId, Friend friend)
        {
            var history = await Data.History.GetFriendHistLends(userId, friend.Id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLend>()).CreateMapper();
            var lends = new List<HistLend>();
            foreach (var item in history)
            {
                var lend = mapper.Map<HistoricalLend, HistLend>(item.Key);
                lend.Friend = friend;
                lend.Thing = item.Value;
                lends.Add(lend);
            }
            return lends;
        }

        /// <summary>
        /// Gets the active lends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friend">The friend.</param>
        /// <returns></returns>
        private async Task<IEnumerable<ActiveLend>> GetActiveLends(Guid userId, Friend friend)
        {
            var things = await Data.Things.GetThingsForFriend(userId, friend.Id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Lend, ActiveLend>()).CreateMapper();
            var lends = new List<ActiveLend>();
            foreach (var thing in things)
            {
                var lend = mapper.Map<ActiveLend>(thing.Lend);
                lend.Friend = friend;
                lend.Thing = thing;
            }
            return lends;
        }
    }
}
