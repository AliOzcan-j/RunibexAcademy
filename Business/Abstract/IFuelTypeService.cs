using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFuelTypeService
    {
        IDataResult<List<FuelType>> GetAll();
        IResult Add(FuelType entity);
        IResult Update(FuelType entity);
        IResult Delete(FuelType entity);
        IDataResult<FuelType>? GetById(int id);
        IDataResult<FuelType>? GetByName(string name);
    }
}
