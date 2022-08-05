using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{//THIS AND ALL IT'S ASSOCIATED CLASSES WILL EITHER BE GONE OR REFACTORED WITH A PROPER CRYPTOGRAPHY ALGORITHM FOR ENCRYPTION AND DECRYPTION
    public class CreditCardManager : ICreditCardService
    {
        ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        public IResult Add(CreditCardForStoreDto creditCardForStoreDto)
        {
            var creditCard = EncryptCreditCard(creditCardForStoreDto);
            _creditCardDal.Add(creditCard);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            throw new NotImplementedException();
            //I now regret using hashing to encrypt this, as i've found out hashing algorithms are pretty much a one-way mailout
        }

        public IDataResult<CreditCard> Get(int id)
        {
            throw new NotImplementedException();
        }

        private CreditCard EncryptCreditCard(CreditCardForStoreDto creditCardForStoreDto)
        {
            byte[] CardNumberHash, CardNumberSalt;
            byte[] CardHolderNameHash, CardHolderNameSalt;
            byte[] ExpirationDateHash, ExpirationDateSalt;

            HashingHelper.CreateHash(creditCardForStoreDto.CardNumber, out CardNumberHash, out CardNumberSalt);
            HashingHelper.CreateHash(creditCardForStoreDto.CardHolderName, out CardHolderNameHash, out CardHolderNameSalt);
            HashingHelper.CreateHash(creditCardForStoreDto.ExpirationMonth + "/" + creditCardForStoreDto.ExpirationYear, out ExpirationDateHash, out ExpirationDateSalt);
            var creditCard = new CreditCard()
            {
                CardNumberHash = CardNumberHash,
                CardNumberSalt = CardNumberSalt,
                CardHolderNameHash = CardHolderNameHash,
                CardHolderNameSalt = CardHolderNameSalt,
                ExpirationDateHash = ExpirationDateHash,
                ExpirationDateSalt = ExpirationDateSalt
            };
            return creditCard;
        }
    }
}
