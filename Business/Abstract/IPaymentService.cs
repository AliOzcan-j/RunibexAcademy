using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IDataResult<List<Payment>> GetAll();
        IResult Add(Payment entity);
        IResult Update(Payment entity);
        IResult Delete(Payment entity);
        IDataResult<Payment>? GetById(int id);
        IDataResult<List<Payment>> GetByUserId(int id);
        IDataResult<List<PaymentDetailDto>> GetPaymentDetails(Expression<Func<PaymentDetailDto, bool>> filter = null);
    }
}
