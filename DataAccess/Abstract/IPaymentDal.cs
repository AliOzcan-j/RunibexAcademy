using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.Payment;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IPaymentDal : IEntityRepository<Payment>
    {
    }
}
