using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
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

            IResult result = BusinessRules.Run(CheckIfReturnedNull(entity.Id));

            if(result != null)
            {
                return result;
            }

            _brandDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(Brand entity)
        {
            _brandDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<Brand> GetByName(string name)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Name == name));
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAllWithoutTracker());
        }

        public IResult Update(Brand entity)
        {
            _brandDal.Update(entity);
            return new SuccessResult();
        }

        private IResult CheckIfReturnedNull(int id)
        {
            var result = _brandDal.Get(p => p.Id == id);
            if (result != null)
            {
                return new ErrorResult(Messages.BrandAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
