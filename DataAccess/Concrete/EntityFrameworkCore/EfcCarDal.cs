using Core.DataAccess.EntityFrameworkCore;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
                             join b in context.Brands on c.BrandId equals b.Id
                             join m in context.Models on c.ModelId equals m.Id
                             join co in context.Colors on c.ColorId equals co.Id
                             join ft in context.FuelTypes on c.FuelTypeId equals ft.Id

                             select new CarDetailDto()
                             {
                                 BrandName = b.Name,
                                 ModelName = $"{m.NamePrefix} {m.NameSuffix}",
                                 ColorName = co.Name,
                                 FuelType = ft.Name,
                                 DailyPrice = c.DailyPrice,
                                 Transmission = c.Transmission == false ? "Manual" : "Automatic",
                                 Milage = c.MilageLimit == true ? "Limited" : "Limitless",
                                 Description = $"{co.Name} {ft.Name} {b.Name} {m.NamePrefix} {m.NameSuffix}"
                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }

        //public new void Add(Car car)
        //{
        //    using (AcademyContext context = new AcademyContext())
        //    {
        //        var model = context.Models
        //            .Include(x => x.Brand)
        //            .Include(x => x.Color)
        //            .Include(x => x.FuelTypes)
        //            .Single(x => x.Id == car.ModelId && x.BrandId == car.);
        //        car.Model = model;

        //        model.Brand.Cars.Add(car);
        //        context.SaveChanges();
        //    }
        //}

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
