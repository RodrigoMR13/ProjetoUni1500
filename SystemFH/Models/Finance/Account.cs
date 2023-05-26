using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SystemFH.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Conta")]
        public string Name { get; set; }

        [DisplayName("Inserido Por:")]
        public int UserId { get; set; }
    }
}
