using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IResult Add(Rental entity);
        IResult Update(Rental entity);
        IResult Delete(Rental entity);
        IDataResult<Rental>? GetById(int id);
        IDataResult<List<RentalDetailDto>> GetCarDetails(Expression<Func<RentalDetailDto, bool>> filter = null);
    }
}
