﻿using System;
using System.Collections.Generic;

namespace ThingsBook.Data.Interface
{
    public interface IUsersBL
    {
        IEnumerable<User> GetUsers();

        User GetUser(Guid id);

        void UpdateUser(User user);

        void DeleteUser(Guid id);

        void CreateUser(User user);

    }
}
