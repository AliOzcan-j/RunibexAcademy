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
    public class PaymentManager : IPaymentService
    {
        IPaymentDal _paymentDal;

        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }

        public IResult Add(Payment entity)
        {
            _paymentDal.Add(entity);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IPaymentService.Get")]
        public IResult Delete(Payment entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Id));

            if (result == null)
            {
                return result;
            }
            _paymentDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAllWithoutTracker());
        }

        [CacheAspect(typeof(DataResult<Payment>))]
        public IDataResult<Payment> GetByCreditCardtId(int id)
        {
            var result = _paymentDal.Get(p => p.CreditCardId == id);
            if (result != null)
            {
                return new SuccessDataResult<Payment>(result);
            }
            return new ErrorDataResult<Payment>();
        }

        [CacheAspect(typeof(DataResult<Payment>))]
        public IDataResult<Payment>? GetById(int id)
        {
            var result = _paymentDal.Get(p => p.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Payment>(result);
            }
            return new ErrorDataResult<Payment>();
        }

        [CacheAspect(typeof(DataResult<List<Payment>>))]
        public IDataResult<List<Payment>> GetByUserId(int id)
        {
            var result = _paymentDal.GetAll(p => p.UserId == id);
            if (result.Any())
            {
                return new SuccessDataResult<List<Payment>> (result);
            }
            return new ErrorDataResult<List<Payment>> ();
        }

        [CacheAspect(typeof(DataResult<List<PaymentDetailDto>>))]
        public IDataResult<List<PaymentDetailDto>> GetPaymentDetails(Expression<Func<PaymentDetailDto, bool>> filter = null)
        {
            var result = _paymentDal.GetPaymentDetails(filter);
            if (result.Any())
            {
                return new SuccessDataResult<List<PaymentDetailDto>>(result);
            }
            return new ErrorDataResult<List<PaymentDetailDto>>();
        }

        [CacheRemoveAspect("IPaymentService.Get")]
        public IResult Update(Payment entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Id));

            if (result == null)
            {
                return result;
            }
            _paymentDal.Update(entity);
            return new SuccessResult();
        }

        private IResult CheckIfExists(int id)
        {
            var result = _paymentDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new ErrorResult(Messages.ThisRecordExists);
            }
            return new SuccessResult();
        }
    }
}
