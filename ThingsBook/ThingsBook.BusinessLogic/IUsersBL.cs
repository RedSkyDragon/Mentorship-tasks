﻿using System;
using System.Threading.Tasks;
using ThingsBook.Data.Interface;

namespace ThingsBook.BusinessLogic
{
    public interface IUsersBL
    {
        Task Create(User user);

        Task Delete(Guid id);

        Task<User> Get(Guid id);

        Task CreateOrUpdate(User user);

        Task Update(User user);
    }
}
