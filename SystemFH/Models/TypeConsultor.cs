using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SystemFH.Models
{
    public class TypeConsultor
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Tipo")]
        public string Name { get; set; }
        [DisplayName("Valor Tarifa")]
        public float Fee { get; set; }
    }
}
