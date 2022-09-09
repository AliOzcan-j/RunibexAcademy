using Core.DataAccess.EntityFrameworkCore;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFrameworkCore
{
    public class EfcUserDal : EFCoreEntityRepositoryBase<User, AcademyContext>, IUserDal
    {
        public new void Delete(User user)
        {
            user.Stasus = false;
            base.Update(user);
        }

        public List<UserDetailDto> GetUserDetails(Expression<Func<UserDetailDto, bool>> filter = null)
        {
            using (AcademyContext context = new AcademyContext())
            {
                var result = from u in context.Users
                             select new UserDetailDto()
                             {
                                 UserName = $"{u.FirstName} {u.LastName}",
                                 Email = u.Email,
                                 ContactNumber = $"{u.Country.CountryCode}{u.ContactNumber}",
                                 Country = $"{u.Country.Name}",
                                 Stasus = u.Stasus
                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
