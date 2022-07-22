using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public string ContactNumber { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool? Stasus { get; set; } = true;
        public Supplier Supplier { get; set; }
        public Country Country { get; set; }
        public List<OperationClaim> OperationClaims { get; set; }
        public List<CreditCard> CreditCards { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
