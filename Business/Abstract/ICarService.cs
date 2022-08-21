using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IResult Add(Car entity);
        IResult Update(Car entity);
        IResult Delete(Car entity);
        IDataResult<Car>? GetById(int id);
        IDataResult<List<Car>> GetBySupplierId(int id);
        IDataResult<List<Car>> GetByBrandId(int id);
        IDataResult<List<Car>> GetByModelId(int id);
        IDataResult<List<Car>> GetByColorId(int id);
        IDataResult<List<Car>> GetByFuleTypeId(int id);
        IDataResult<List<CarDetailDto>> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null);
    }
}
