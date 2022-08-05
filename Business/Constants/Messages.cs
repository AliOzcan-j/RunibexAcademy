using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        internal static readonly string UserRegistered = "User have been Registered";
        internal static readonly string SuccessfulLogin = "Login was Successful";
        internal static readonly string ThisRecordExists = "This Record Exists";
        internal static readonly string UserDoesntExists = "This User Doesn't Exists";
        internal static readonly string UserAlreadyExists = "This User Already Exists";
        internal static readonly string UserNotFound = "This user could not be found";
        internal static readonly string IncorrectPassword = "Incorrect password";
    }
}
