using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFuelTypeService
    {
        IDataResult<List<FuelType>> GetAll();
        IDataResult<FuelType> GetByName(string name);
        IResult Add(FuelType entity);
        IResult Update(FuelType entity);
        IResult Delete(FuelType entity);
    }
}
