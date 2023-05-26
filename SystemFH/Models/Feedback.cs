using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemFH.Models
{
    public class Feedback
    {
		[Key]
		public int Id { get; set; }
        [DisplayName("Data")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [DisplayName("Turma/Círculo")]
        public int CircleId { get; set; }
        [DisplayName("Turma/Círculo")]
        public Circle Circle { get; set; }
		public int ThemeId { get; set; }
        [DisplayName("Tema")]
        public Theme Theme { get; set; }
        [DisplayName("Oportunidade de Aprendizado")]
        public string OportunityLearning { get; set; }
        [DisplayName("Nota")]
        public float Note { get; set; }
        [DisplayName("Comentário")]
        public string Comment { get; set; }

        [DisplayName("Inserido Por:")]
        public int UserId { get; set; }

        public List<PersonFeedback> PersonFeedbacks { get; set; }

        [NotMapped]
        [DisplayName("Aluno")]
        public int StudentId { get; set; }
        [NotMapped]
        [DisplayName("Aluno")]
        public Person StudentPerson { get; set; }
        [NotMapped]
        [DisplayName("Professor")]
        public int TeacherId { get; set; }
        [NotMapped]
        [DisplayName("Professor")]
        public Person TeacherPerson { get; set; }
    }

}
