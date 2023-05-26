using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SystemFH.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Categoria")]
        public string Name { get; set; }

        [DisplayName("Inserido Por:")]
        public int UserId { get; set; }
    }
}
