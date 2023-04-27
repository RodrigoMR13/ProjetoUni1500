using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SystemFH.Models
{
    public class Enterprise
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Empresa")]
        public string Name { get; set; }
        public string CNPJ { get; set; }
        [DisplayName("Telefone/Celular")]
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
