using System;
using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class RoleDal : EfEntityRepositoryBase<Role, BuilderContext>, IRoleDal
    {
        
    }
}

