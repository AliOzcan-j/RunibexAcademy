using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CountryManager : ICountryService
    {
        ICountryDal _countryDal;

        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }

        public IResult Add(Country entity)
        {
            _countryDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(Country entity)
        {
            _countryDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Country>> GetAll()
        {
            return new SuccessDataResult<List<Country>>(_countryDal.GetAllWithoutTracker());
        }

        public IDataResult<Country>? GetById(int id)
        {
            return new SuccessDataResult<Country>(_countryDal.Get(c => c.Id == id));
        }

        public IDataResult<Country> GetByIsoCode(string isoCode)
        {
            return new SuccessDataResult<Country>(_countryDal.Get(c => c.IsoCode.Equals(isoCode)));
        }

        public IDataResult<Country>? GetByName(string name)
        {
            return new SuccessDataResult<Country>(_countryDal.Get(c => c.Name.Equals(name)));
        }

        public IResult Update(Country entity)
        {
            _countryDal.Update(entity);
            return new SuccessResult();
        }
    }
}
