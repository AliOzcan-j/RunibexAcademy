using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.Rental;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll(Expression<Func<Rental, bool>> filter = null);
        IResult Add(Rental entity);
        IResult Update(Rental entity);
        IResult Delete(Rental entity);
        IDataResult<Rental>? GetById(int id);
    }
}
