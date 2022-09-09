using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Car
{
    public class CarDtoForUpdate : IDto
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int FuelTypeId { get; set; }
        public bool? MilageLimit { get; set; }
        public bool Transmission { get; set; }
        public decimal DailyPrice { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
