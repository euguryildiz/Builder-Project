using System;
using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class GroupDal : EfEntityRepositoryBase<Group, BuilderContext>, IGroupDal
    {
        
    }
}

