using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IFuelTypeService
    {
        IDataResult<List<FuelType>> GetAll(Expression<Func<FuelType, bool>> filter = null);
        IResult Add(FuelType entity);
        IResult Update(FuelType entity);
        IResult Delete(FuelType entity);
        IDataResult<FuelType>? GetById(int id);
        IDataResult<FuelType>? GetByName(string name);
    }
}
