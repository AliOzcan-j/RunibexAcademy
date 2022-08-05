using Business.Abstract;
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

        public IResult Delete(Payment entity)
        {
            _paymentDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAllWithoutTracker());
        }

        public IDataResult<Payment> GetByCreditCardtId(int id)
        {
            return new SuccessDataResult<Payment>(_paymentDal.Get(p => p.CreditCardId == id));
        }

        public IDataResult<Payment>? GetById(int id)
        {
            return new SuccessDataResult<Payment>(_paymentDal.Get(p => p.Id == id));
        }

        public IDataResult<List<Payment>> GetByUserId(int id)
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll(p => p.UserId == id));
        }

        public IDataResult<List<PaymentDetailDto>> GetPaymentDetails(Expression<Func<PaymentDetailDto, bool>> filter = null)
        {
            return new SuccessDataResult<List<PaymentDetailDto>>(_paymentDal.GetPaymentDetails(filter));
        }

        public IResult Update(Payment entity)
        {
            _paymentDal.Update(entity);
            return new SuccessResult();
        }
    }
}
