using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Entities.Concrete;
using Entities.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;

        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        [CacheAspect(typeof(DataResult<User>))]
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            IResult result = BusinessRules.Run(CheckIfUserExists(userForLoginDto.Email));
            if (!result.Success)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            var userToCheck = _userService.GetByEmail(userForLoginDto.Email);
            if (!HashingHelper.VerifyHashValue(userForLoginDto.Email, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.IncorrectPassword);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            IResult result = BusinessRules.Run(CheckIfUserExists(userForRegisterDto.Email));
            if (!result.Success)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreateHash(userForRegisterDto.Email, out passwordHash, out passwordSalt);
                var user = new User()
                {
                    Email = userForRegisterDto.Email,
                    FirstName = userForRegisterDto.FirstName,
                    LastName = userForRegisterDto.LastName,
                    CountryId = userForRegisterDto.CountryId,
                    ContactNumber = userForRegisterDto.ContactNumber,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Stasus = true
                };
                _userService.Add(user);
                return new SuccessDataResult<User>(user, Messages.UserRegistered);
            }
            return new ErrorDataResult<User>(result.Message);
        }

        private IResult CheckIfUserExists(string email)
        {
            if (_userService.GetByEmail(email).Success)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.UserAlreadyExists);
        }
    }
}
