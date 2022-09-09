using Core.DataAccess.EntityFrameworkCore;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Car;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFrameworkCore
{
    public class EfcCarDal : EFCoreEntityRepositoryBase<Car, AcademyContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (AcademyContext context = new AcademyContext())
            {
                var result = from c in context.Cars
                             //join b in context.Brands on c.BrandId equals b.Id
                             join m in context.Models on c.ModelId equals m.Id
                             join co in context.Colors on c.ColorId equals co.Id
                             join ft in context.FuelTypes on c.FuelTypeId equals ft.Id

                             select new CarDetailDto()
                             {
                                 BrandName = m.Brand.Name,
                                 ModelName = $"{m.NamePrefix} {m.NameSuffix}",
                                 ColorName = co.Name,
                                 FuelType = ft.Name,
                                 DailyPrice = c.DailyPrice,
                                 Transmission = c.Transmission == false ? "Manual" : "Automatic",
                                 Milage = c.MilageLimit == true ? "Limited" : "Limitless",
                                 Description = $"{co.Name} {ft.Name} {m.Brand.Name} {m.NamePrefix} {m.NameSuffix}"
                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }

        public new void Add(Car car)
        {
            using (AcademyContext context = new AcademyContext())
            {
                var brand = context.Brands
                    .Include(x => x.Models)
                    .ThenInclude(x => x.Cars)
                    .SingleOrDefault(x => x.Id == car.Model.BrandId);

                car.Model.Brand = brand;
                context.Add(car);
                context.SaveChanges();
            }
        }

        public new void Update(Car car)
        {
            using (AcademyContext context = new AcademyContext())
            {
                var tracked = context.Cars
                    .Include(x => x.Model)
                    .ThenInclude(x => x.Brand)
                    .SingleOrDefault(x => x.Id == car.Id && x.Model.Id == car.ModelId);

                context.Entry(tracked).CurrentValues.SetValues(car);
                context.SaveChanges();
            }
        }

        public new bool Delete(Car car)
        {
            using (AcademyContext context = new AcademyContext())
            {
                var carToDelete = context.Cars.Single(x => x.Id == car.Id);
                if( carToDelete != null)
                {
                    carToDelete.IsDeleted = true;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
