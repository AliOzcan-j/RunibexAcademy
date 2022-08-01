using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFuelTypeService : IEntityServiceBase<FuelType>, IEntityServiceAddon<FuelType>
    {
    }
}
