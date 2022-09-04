using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class GroupManager : IGroupService
    {
        private IGroupDal _groupDal;

        public GroupManager(IGroupDal groupDal)
        {
            _groupDal = groupDal;
        }

        public IDataResult<List<Group>> GetIdList(int id)
        {
            var result = _groupDal.GetAll(x => x.ParentId == id);

            return new SuccessDataResult<List<Group>>(result);
            
        }

    }
}

