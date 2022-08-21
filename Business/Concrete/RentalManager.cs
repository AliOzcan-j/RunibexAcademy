using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental entity)
        {
            _rentalDal.Add(entity);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Id));
            if(result == null)
            {
                return result;
            }
            _rentalDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAllWithoutTracker());
        }

        [CacheAspect(typeof(DataResult<Rental>))]
        public IDataResult<Rental>? GetById(int id)
        {
            var result = _rentalDal.GetWithoutTracker(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Rental>(result);
            }
            return new ErrorDataResult<Rental>();
        }

        [CacheAspect(typeof(DataResult<List<RentalDetailDto>>))]
        public IDataResult<List<RentalDetailDto>> GetCarDetails(Expression<Func<RentalDetailDto, bool>> filter = null)
        {
            var result = _rentalDal.GetRentalDetails(filter);
            if (result.Any())
            {
                return new SuccessDataResult<List<RentalDetailDto>>(result);
            }
            return new ErrorDataResult<List<RentalDetailDto>>();

        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Id));
            if (result == null)
            {
                return result;
            }
            _rentalDal.Update(entity);
            return new SuccessResult();
        }

        private IResult CheckIfExists(int id)
        {
            var result = _rentalDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new ErrorResult(Messages.ThisRecordExists);
            }
            return new SuccessResult();
        }
    }
}
