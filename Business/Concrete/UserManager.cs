using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Queue;
using Core.Utilities.Business.Concrete;
using Core.Utilities.MessageBrokers.Events;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [QueueAfterAspect(typeof(UserRegisteredEvent))]
        public IResult Add(User entity)
        {
            _userDal.Add(entity);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Email));
            if (result == null)
            {
                return result;
            }
            _userDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<User>> GetAll(Expression<Func<User, bool>> filter = null)
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAllWithoutTracker(filter));
        }

        [CacheAspect(typeof(DataResult<User>))]
        public IDataResult<User> GetByEmail(string email)
        {
            var result = _userDal.Get(u => u.Email.Equals(email));
            if (result != null)
            {
                return new SuccessDataResult<User>();
            }
            return new ErrorDataResult<User>(Messages.UserDoesntExists);
        }

        [CacheAspect(typeof(DataResult<User>))]
        public IDataResult<User>? GetById(int id)
        {
            var result = _userDal.Get(u => u.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<User>(result);
            }
            return new ErrorDataResult<User>();
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Update(User entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Email));
            if (result == null)
            {
                return result;
            }
            _userDal.Update(entity);
            return new SuccessResult();
        }

        private IResult CheckIfExists(string email)
        {
            var result = _userDal.Get(x => x.Email.Equals(email));
            if (result != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
