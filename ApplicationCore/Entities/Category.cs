using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Lot> Lots { get; set; }

    }
}