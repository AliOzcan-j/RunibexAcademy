using Core.DataAccess.EntityFrameworkCore;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Payment;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFrameworkCore
{
    public class EfcPaymentDal : EFCoreEntityRepositoryBase<Payment, AcademyContext>, IPaymentDal
    {

        public new void Delete(Payment payment)
        {
            //No
        }
    }

}
