using System.Collections.Generic;
using ApplicationCore.Entities;

namespace ApplicationCore.DTO
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<LotDto> Lots { get; set; }
    }
}