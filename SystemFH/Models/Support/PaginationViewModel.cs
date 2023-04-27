using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemFH.Models
{
    [NotMapped]
    public class PaginationViewModel<T>
    {
        public List<T> Items { get; set; } //Generalização
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }  
    }
}
