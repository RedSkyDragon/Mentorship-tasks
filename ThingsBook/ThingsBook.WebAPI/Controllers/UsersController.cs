﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThingsBook.Data.Mongo;

namespace ThingsBook.WebAPI.Controllers
{
    public class UsersController : ApiController
    {
        // GET: api/Users
        public IHttpActionResult Get()
        {
            var users = new Users(new ThingsBookContext());
            return Ok(users.GetUsers());
        }

        // GET: api/Users/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
