using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemFH.Models
{

	public enum Status
    {
		BRANCO,
		CONCLUIDO
    }
    public class Learning // Plano Aprendizado
    {
		[Key]
		public int Id { get; set; }
        [DisplayName("Turma/Círculo")]
        public int CircleId { get; set; }
        [DisplayName("Turma/Círculo")]
        public Circle Circle { get; set; }
        [DisplayName("Tema")]
        public int ThemeId { get; set; }
		[DisplayName("Tema")]
		public Theme Theme{ get; set; }
        [DisplayName("Oportunidade de Aprendizado")]
        public string OportunityLearning { get; set; }
        [DisplayName("Ação de Aprendizado")]
        public string LearningAction { get; set; }
        [DisplayName("Data de Mensuração")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd/MM/yyyy}")]
        public DateTime MeasurementDate { get; set; }
        [DisplayName("Forma de Mensuração")]
        public float MeasurementForm { get; set; }
        [DisplayName("Resultado")]
        public string Result { get; set; }
        [DisplayName("Comentários")]
        public string Comment { get; set; }
        [DisplayName("Situação")]
        public Status Status { get; set; }
		
		public List<PersonLearning> PeopleLearning { get; set; }

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
