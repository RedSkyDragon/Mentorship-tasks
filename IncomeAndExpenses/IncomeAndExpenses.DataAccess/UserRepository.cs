using IncomeAndExpenses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncomeAndExpenses.Web.Models
{
    public class UserRepository : IRepository<string, User>
    {
        private InAndExDbContext _db;

        public UserRepository(InAndExDbContext db)
        {
            _db = db;
        }

        public void Create(User item)
        {
            _db.Set<User>().Add(item);
        }

        public void Delete(string id)
        {
            User user = _db.Set<User>().Find(id);
            if (user != null)
            {
                _db.Set<User>().Remove(user);
            }
        }

        public User Get(string id)
        {
            return _db.Set<User>().Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Set<User>();
        }

        public void Update(User item)
        {
            var user = _db.Set<User>().Find(item.Id);
            if (user != null)
            {
                _db.Entry(user).CurrentValues.SetValues(item);
            }
        }
    }
}