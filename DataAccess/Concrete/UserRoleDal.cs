using System;
using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class UserRoleDal : EfEntityRepositoryBase<UserRole, BuilderContext>, IUserRoleDal
    {
    }
}

