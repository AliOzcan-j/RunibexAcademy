using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICountryService
    {
        IDataResult<List<Country>> GetAll();
        IResult Add(Country entity);
        IResult Update(Country entity);
        IResult Delete(Country entity);
        IDataResult<Country>? GetById(int id);
        IDataResult<Country>? GetByName(string name);
        IDataResult<Country> GetByIsoCode(string isoCode);
    }
}
