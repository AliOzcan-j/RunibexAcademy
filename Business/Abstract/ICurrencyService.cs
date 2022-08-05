using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICurrencyService : IEntityServiceBase<Currency>, IEntityServiceAddon<Currency>
    {
        IDataResult<Currency> GetByIsoCode(string isoCode);
    }
}
