using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemFH.Models
{
    public class PersonLearning
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Tipo")]
        public TypePerson Type { get; set; }
        [DisplayName("Pessoa")]
        public int PersonId { get; set; }
        [DisplayName("Pessoa")]
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        [DisplayName("Aprendizado")]
        public int LearningId { get; set; }
        [DisplayName("Aprendizado")]
        public Learning Learning { get; set; }
    }
}
