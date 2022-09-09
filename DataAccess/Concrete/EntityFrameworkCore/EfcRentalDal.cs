using Core.DataAccess.EntityFrameworkCore;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Rental;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFrameworkCore
{
    public class EfcRentalDal : EFCoreEntityRepositoryBase<Rental, AcademyContext>, IRentalDal
    {
        public new void Add(Rental rental)
        {
            rental.IsActive = true;
            using var context = new AcademyContext();
            using (var transaction = context.Database.BeginTransaction())
            {
                context.Payments.Add(rental.Payment);
                context.SaveChanges();

                context.Rentals.Add(rental);
                context.SaveChanges();

                transaction.Commit();
            }
        }

        public new void Delete(Rental rental)
        {
            using (AcademyContext context = new AcademyContext())
            {
                var rentalToDelete = context.Rentals.SingleOrDefault(x => x.CarId == rental.CarId);
                rentalToDelete.ReturnDate = DateTime.Now;
                rentalToDelete.IsActive = false;
                base.Update(rentalToDelete);
            }
            //rental.ReturnDate = DateTime.Now;
            //rental.IsActive = false;
            //base.Update(rental);
        }
    }
}
