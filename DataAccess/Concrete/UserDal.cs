using System;
using System.Linq.Expressions;
using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Security.Cryptography;
using Core.Utilities.Hash;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class UserDal : EfEntityRepositoryBase<User, BuilderContext>, IUserDal
    {
        public User GetUserDetail(int Id)
        {
            using (BuilderContext context = new BuilderContext())
            {
                return context.Users.Include(s => s.UserRoles).ThenInclude(x => x.Role).FirstOrDefault();

                var Query = context.Users
                .Include(i => i.UserRoles.Select(s => s.Role)).AsNoTracking()
                .ToList();
                return Query.Where(x => x.Id == Id).FirstOrDefault();

            }
        }
    }
}

