using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SystemFH.Models
{
    public class PersonFeedback
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
        [DisplayName("Feedback")]
        public int FeedbackId { get; set; }
        [DisplayName("Feedback")]
        public Feedback Feedback { get; set; }
    }
}
