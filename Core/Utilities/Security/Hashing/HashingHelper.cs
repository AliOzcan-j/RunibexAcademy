using Core.Utilities.Helpers.FileHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper : IHelperBase
    {
        //out keyword'ü C dilindeki adres operatörleri ve pointerlar gibi çalışıyor. Bu keyword kullanıldığında bu nesne üzerindeki değişimler referansın gösterdiği yerdeki verininde değişmesini sağlıyor
        //Yani metoda dönüş tipi verilmeden, doğrudan verinin orjinali üzerinde işlem yapılıyor kopyasında değil
        public static void CreateHash(string message, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;//şifreleme sistemimizin belirlenen key'i
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            }
        }

        public static bool VerifyHashValue(string message, byte[] hash, byte[] salt)
        {//şifre oluşturulurken algoritmaya verilen key verisini tekrar algoritmaya beslediğimizde aynı hash değerini verir.
         //Burda key verimiz passwordSalt diye isimlendirilen değişken
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));//gelen düz metin ile yine bir hash oluşturuluyor
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hash[i])//byte by byte elimizdeki hash ile oluşturulan hash i karşılaştırarak şifrenin doğru olup olmadığı sorgulanıyor
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
