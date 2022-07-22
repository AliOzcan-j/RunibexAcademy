using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Add(Brand entity)
        {
            _brandDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(Brand entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Brand> Get(Expression<Func<Brand, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Brand>> GetAll(Expression<Func<Brand, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public IResult Update(Brand entity)
        {
            throw new NotImplementedException();
        }
    }
}
