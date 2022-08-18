using Core.DataAccess.EntityFrameworkCore;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFrameworkCore
{
    public class EfcRentalDal : EFCoreEntityRepositoryBase<Rental, AcademyContext>, IRentalDal
    {
        public new void Add(Rental rental)
        {
            rental.IsActive = true;
            base.Add(rental);
        }

        public new void Delete(Rental rental)
        {
            rental.IsActive = false;
            base.Update(rental);
        }

        public List<RentalDetailDto> GetRentalDetails(Expression<Func<RentalDetailDto, bool>> filter = null)
        {
            using (AcademyContext context = new AcademyContext())
            {
                var result = from r in context.Rentals

                             select new RentalDetailDto()
                             {
                                 UserName = $"{r.User.FirstName} {r.User.LastName}",
                                 CarDetail = $"{r.Car.FuelType.Name} {r.Car.Color.Name} {r.Car.Model.Brand.Name} {r.Car.Model.NamePrefix} {r.Car.Model.NameSuffix}",
                                 SupplierName = $"{r.Supplier.CompanyName}",
                                 Payment = r.Payment.Amount,
                                 RentDate = r.RentDate,
                                 isReturned = !r.IsActive
                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
