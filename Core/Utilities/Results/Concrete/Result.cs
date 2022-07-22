using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public bool Success { get; }
        public string Message { get; }

        public Result(bool success, string message):this(success)//Mesajı ve sonucu dönderir. Yalnızca sonuç isteniyorsa this(success) aşağıdaki metoda yönlendirir
        {
            Success = success;
            Message = message;
        }
        public Result(bool success)
        {
            Success=success;
        }
    }
}
