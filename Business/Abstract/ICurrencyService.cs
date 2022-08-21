using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICurrencyService
    {
        IDataResult<List<Currency>> GetAll();
        IResult Add(Currency entity);
        IResult Update(Currency entity);
        IResult Delete(Currency entity);
        IDataResult<Currency>? GetById(int id);
        IDataResult<Currency>? GetByName(string name);
        IDataResult<Currency> GetByIsoCode(string isoCode);
    }
}
