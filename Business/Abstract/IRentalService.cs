using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IRentalService : IEntityServiceBase<Rental>
    {
        IDataResult<List<RentalDetailDto>> GetCarDetails(Expression<Func<RentalDetailDto, bool>> filter = null);
    }
}
