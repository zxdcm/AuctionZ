using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
    public class LotsFilterCriteria
    {
        public int? Category { get; set; }
        public string Title { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 4;
        public bool? Active { get; set; } = null;
        public int? UserId { get; set; } = null;
    }
}
