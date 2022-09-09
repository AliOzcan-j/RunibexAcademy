using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.Payment;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IDataResult<List<Payment>> GetAll(Expression<Func<Payment, bool>> filter = null);
        IResult Add(Payment entity);
        IResult Update(Payment entity);
        IResult Delete(Payment entity);
        IDataResult<Payment>? GetById(int id);
        IDataResult<List<Payment>> GetByUserId(int id);
    }
}
