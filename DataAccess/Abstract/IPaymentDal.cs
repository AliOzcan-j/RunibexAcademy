using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IPaymentDal : IEntityRepository<Payment>
    {
        public List<PaymentDetailDto> GetPaymentDetails(Expression<Func<PaymentDetailDto, bool>> filter = null);
    }
}
