using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IResult Add(User entity);
        IResult Update(User entity);
        IResult Delete(User entity);
        IDataResult<User>? GetById(int id);
        IDataResult<User> GetByEmail(string email);
    }
}
