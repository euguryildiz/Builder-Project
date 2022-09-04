using System;
using Business.Abstract;
using Constants.Message;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Hash;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Aspects.Autofac.Validation;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using PagedList.Core;
using Entities.FrontEnd;
using System.Linq.Expressions;
using Core.FrontEnd;
using System.Data.Common;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private IUserRoleDal _userRole;
        private IRoleDal _roleDal;
        //private  BaseManager<User> _userBaseService = new BaseManager<User>(;

        public UserManager(IUserDal userDal, IUserRoleDal userRole, IRoleDal roleDal)
        {
            _userDal = userDal;
            _userRole = userRole;
            _roleDal = roleDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IDataResult<User> AddUser(User user)
        {

            throw new NotImplementedException();
        }

        #region User 
        public IDataResult<User> GetUser(int Id)
        {

            var result = _userDal.Get(x => x.Id == Id && !x.IsDeleted);

            if (result == null)
            {
                return new ErrorDataResult<User>(null, Messages.RecordNotFound);
            }

            return new SuccessDataResult<User>(result);
        }

        public IDataResult<User> AdminLogin(string username, string password)
        {
            try
            {

                var pass = Encryption.SHA1Hash(password);
                var User = _userDal.GetQueryInclude(x => x.Username == username && x.Password == pass && !x.IsDeleted,"UserRoles,UserRoles.Role");

                if (User != null)
                {
                    //var userDetail = _userDal.GetUserDetail(User.Id);
                    if (User.IsActive == false)
                    {
                        return new ErrorDataResult<User>(Messages.UserPassive);
                    }
                    var userRoles = _userRole.GetAllQueryInclude(x=> !x.IsDeleted && x.Id == User.Id,"Role");
                    if (!userRoles.Exists(x => x.Role?.IsAdmin == true))
                    {
                        return new ErrorDataResult<User>(Messages.AdminNotAuthorization);
                    }

                    return new SuccessDataResult<User>(User);

                }

                else
                {
                    return new ErrorDataResult<User>(Messages.LoginUserNotFound);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<User>(ex.Message);
            }
            

        }

        public IDataResult<IPagedList<User>> GetAllUsers(int page = 1, int pageSize = 25)
        {
            var result = _userDal.PagedQuery(page, pageSize, new Query<User>().Where(a => !a.IsDeleted).Select(x => new User()
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Username = x.Username,
                IsActive = x.IsActive,
                UserRoles = x.UserRoles.Where(t => !t.IsDeleted).Select(c => new UserRole()
                {
                    RoleId = c.RoleId,
                    UserId = c.UserId,
                    Role = new Role()
                    {
                        Subject = c.Role.Subject,
                        IsAdmin = c.Role.IsAdmin
                    }
                }).ToList()
            }));

            return new SuccessDataResult<IPagedList<User>>(result);
        }

        public IResult User_Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult();
        }

        #endregion


    }
}

