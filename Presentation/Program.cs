using Business.Abstract;
using Business.Concrete;
using Core.Entities.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFrameworkCore;
using Entities.Concrete;

ICarDal carDal = new EfcCarDal();
ICarService carService = new CarManager(carDal);

IModelDal modelDal = new EfcModelDal();
IModelService modelService = new ModelManager(modelDal);

IRentalService rentalService = new RentalManager(new EfcRentalDal());

using (var context = new AcademyContext())
{
    //var brand = context.Brands.Single(x => x.Id == 1);
    //var color = context.Colors.Single(x => x.Name.Equals("Black"));
    //var fuelType = context.FuelTypes.Single(x => x.Id == 4);
    //var model = context.Models.Single(x => x.Id == 1);
    //carService.Add(new Car()
    //{
    //    Brand = brand,
    //    Model = model,
    //    Color = color,
    //    FuelType = fuelType,
    //    MilageLimit = true,
    //    DailyPrice = 500
    //});

    carService.Delete(context.Cars.Single(x => x.Id == 2));
}



Console.WriteLine();