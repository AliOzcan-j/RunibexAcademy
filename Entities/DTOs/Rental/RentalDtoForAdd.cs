using Core.Entities.Abstract;
using Entities.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Rental
{
    public class RentalDtoForAdd : IDto
    {
        public int CarId { get; set; }
        public int UserId { get; set; }
        public int SupplierId { get; set; }
        //public int PaymentId { get; set; } //stripe implementasyonundan sonra kullanılabilir
        public bool MilageLimit { get; set; }
        public PaymentDtoForAdd PaymentDtoForAdd { get; set; }
    }
}
