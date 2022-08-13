using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface ICarService : IEntityServiceBase<Car>
    {
        IDataResult<List<Car>> GetSupplierId(int id);
        IDataResult<List<Car>> GetBrandId(int id);
        IDataResult<List<Car>> GetModelId(int id);
        IDataResult<List<Car>> GetColorId(int id);
        IDataResult<List<Car>> GetFuleTypeId(int id);
        IDataResult<List<CarDetailDto>> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null);
    }
}
