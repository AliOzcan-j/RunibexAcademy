using Core.DataAccess.EntityFrameworkCore;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CarImage;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFrameworkCore
{
    public class EfcCarImageDal : EFCoreEntityRepositoryBase<CarImage, AcademyContext>, ICarImageDal
    {
        public List<CarImageListDto> GetCarImageList(Expression<Func<CarImageListDto, bool>> filter = null)
        {
            using (AcademyContext context = new AcademyContext())
            {
                var result = from ci in context.CarImages
                             join ca in context.Cars on ci.CarId equals ca.Id
                             select new CarImageListDto()
                             {
                                 CarId = ca.Id,
                                 ImagePath = ci.ImagePath
                             };
                return filter == null
                ? result.ToList()
                : result.Where(filter).ToList();
            };
        }
    }
}
