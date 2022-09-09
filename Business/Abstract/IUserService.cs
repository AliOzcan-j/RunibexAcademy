using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll(Expression<Func<User, bool>> filter = null);
        IResult Add(User entity);
        IResult Update(User entity);
        IResult Delete(User entity);
        IDataResult<User>? GetById(int id);
        IDataResult<User> GetByEmail(string email);
    }
}
