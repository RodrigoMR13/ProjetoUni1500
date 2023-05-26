using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SystemFH.Models
{
    public class CostCenter
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Centro de Custo")]
        public string Name { get; set; }

        [DisplayName("Inserido Por:")]
        public int UserId { get; set; }
    }
}
