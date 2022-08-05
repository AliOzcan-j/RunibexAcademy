using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CurrencyManager : ICurrencyService
    {
        ICurrencyDal _currencyDal;

        public CurrencyManager(ICurrencyDal currencyDal)
        {
            _currencyDal = currencyDal;
        }

        public IResult Add(Currency entity)
        {
            _currencyDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(Currency entity)
        {
            _currencyDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Currency>> GetAll()
        {
            return new SuccessDataResult<List<Currency>>(_currencyDal.GetAllWithoutTracker());
        }

        public IDataResult<Currency>? GetById(int id)
        {
            return new SuccessDataResult<Currency>(_currencyDal.Get(c => c.Id == id));
        }

        public IDataResult<Currency> GetByIsoCode(string isoCode)
        {
            return new SuccessDataResult<Currency>(_currencyDal.Get(c => c.IsoCode.Equals(isoCode)));
        }

        public IDataResult<Currency>? GetByName(string name)
        {
            return new SuccessDataResult<Currency>(_currencyDal.Get(c => c.Name.Equals(name)));
        }

        public IResult Update(Currency entity)
        {
            _currencyDal.Update(entity);
            return new SuccessResult();
        }
    }
}
