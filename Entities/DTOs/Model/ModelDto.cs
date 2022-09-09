using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Model
{
    public class ModelDto : IDto
    {
        public string NamePrefix { get; set; }
        public string? NameSuffix { get; set; }
        public string ModelYear { get; set; }
        public int BrandId { get; set; }
    }
}
