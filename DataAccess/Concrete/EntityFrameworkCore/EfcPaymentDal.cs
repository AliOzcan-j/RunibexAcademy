using Core.DataAccess.EntityFrameworkCore;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFrameworkCore
{
    public class EfcPaymentDal : EFCoreEntityRepositoryBase<Payment, AcademyContext>, IPaymentDal
    {
        public List<PaymentDetailDto> GetPaymentDetails(Expression<Func<PaymentDetailDto, bool>> filter = null)
        {
            using (AcademyContext context = new AcademyContext())
            {
                var result = from p in context.Payments
                             select new PaymentDetailDto()
                             {
                                 UserId = p.UserId,
                                 UserName = $"{p.User.FirstName} {p.User.LastName}",
                                 Amount = p.Amount,
                                 PaymentDate = p.PaymentDate,
                                 RentalId = p.Rental.Id
                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }

        public new void Delete(Payment payment)
        {
            //No
        }
    }

}
