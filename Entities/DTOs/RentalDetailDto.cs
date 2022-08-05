using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RentalDetailDto : IDto
    {
        public string UserName { get; set; }
        public string CarDetail { get; set; }
        public string SupplierName { get; set; }
        public decimal Payment { get; set; }
        public DateTime? RentDate { get; set; }
        public bool isReturned { get; set; }
    }
}
