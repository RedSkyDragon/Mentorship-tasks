﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="storage">The storage.</param>
        public FriendsBL(Storage storage): base(storage) { }

        /// <summary>
        /// Creates the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friend">The friend.</param>
        /// <returns>
        /// Created friend.
        /// </returns>
        public async Task<Models.Friend> Create(Guid userId, Models.Friend friend)
        {
            CheckFriend(friend);
            await Storage.Friends.CreateFriend(userId, ModelsConverter.ToDataModel(friend, userId));
            return ModelsConverter.ToBLModel(await Storage.Friends.GetFriend(userId, friend.Id));
        }

        /// <summary>
        /// Deletes the friend by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="id">The friend identifier.</param>
        /// <returns></returns>
        public async Task Delete(Guid userId, Guid id)
        {
            await Storage.Lends.DeleteFriendLends(userId, id);
            await Storage.History.DeleteFriendHistory(userId, id);
            await Storage.Friends.DeleteFriend(userId, id);
        }

        /// <summary>
        /// Gets all friends for user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List if friends.
        /// </returns>
        public async Task<IEnumerable<Models.Friend>> GetAll(Guid userId)
        {
            var friends = await Storage.Friends.GetFriends(userId);
            return friends.Select(ModelsConverter.ToBLModel);
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
            var friend = ModelsConverter.ToBLModel(await Storage.Friends.GetFriend(userId, friendId));
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
        public async Task<Models.Friend> GetOne(Guid userId, Guid id)
        {
            return ModelsConverter.ToBLModel(await Storage.Friends.GetFriend(userId, id));
        }

        /// <summary>
        /// Updates the friend.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friend">The friend.</param>
        /// <returns>
        /// Updated friend.
        /// </returns>
        public async Task<Models.Friend> Update(Guid userId, Models.Friend friend)
        {
            CheckFriend(friend);
            await Storage.Friends.UpdateFriend(userId, ModelsConverter.ToDataModel(friend, userId));
            return ModelsConverter.ToBLModel(await Storage.Friends.GetFriend(userId, friend.Id));
        }

        /// <summary>
        /// Gets the history lends.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="friend">The friend.</param>
        /// <returns></returns>
        private async Task<IEnumerable<HistLend>> GetHistoryLends(Guid userId, Models.Friend friend)
        {
            var history = await Storage.History.GetFriendHistLends(userId, friend.Id);
            var lendMapper = new MapperConfiguration(cfg => cfg.CreateMap<HistoricalLend, HistLend>()).CreateMapper();
            var thingMapper = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.Thing, Models.Thing>()).CreateMapper();
            var lends = new List<HistLend>();
            foreach (var item in history)
            {
                var lend = lendMapper.Map<HistoricalLend, HistLend>(item.Key);
                lend.Friend = friend;
                lend.Thing = thingMapper.Map<Models.Thing>(item.Value);
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
        private async Task<IEnumerable<ActiveLend>> GetActiveLends(Guid userId, Models.Friend friend)
        {
            var things = await Storage.Things.GetThingsForFriend(userId, friend.Id);
            var lendMapper = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.Lend, ActiveLend>()).CreateMapper();
            var thingMapper = new MapperConfiguration(cfg => cfg.CreateMap<Data.Interface.Thing, Models.Thing>()).CreateMapper();
            var lends = new List<ActiveLend>();
            foreach (var thing in things)
            {
                var lend = lendMapper.Map<ActiveLend>(thing.Lend);
                lend.Friend = friend;
                lend.Thing = thingMapper.Map<Models.Thing>(thing);
                lends.Add(lend);
            }
            return lends;
        }

        /// <summary>
        /// Checks the friend.
        /// </summary>
        /// <param name="friend">The friend.</param>
        /// <exception cref="ArgumentNullException">Friend must not be null. - friend</exception>
        /// <exception cref="ModelValidationException">Friend must not be empty.</exception>
        private void CheckFriend(Models.Friend friend)
        {
            if (friend == null)
            {
                throw new ArgumentNullException(nameof(friend), "Friend must not be null.");
            }
            if (string.IsNullOrEmpty(friend.Name) && string.IsNullOrEmpty(friend.Contacts))
            {
                throw new ModelValidationException("Friend must not be empty.");
            }
        }
    }
}
