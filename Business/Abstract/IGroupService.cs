using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IGroupService
    {
        IDataResult<List<Group>> GetIdList(int id);
    }
}

