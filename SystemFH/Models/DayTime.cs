using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SystemFH.Models
{
    public class DayTime
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Atividade")]
        public int ActualStatusId { get; set; }
        [DisplayName("Atividade")]
        public ActualStatus ActualStatus { get; set; }
        [DisplayName("Data")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }
        [DisplayName("Horas Reais")]
        public float RealTime { get; set; }
        [DisplayName("Finalizado")]
        public bool Delivered { get; set; }

        [DisplayName("Inserido Por:")]
        public int UserId { get; set; }
    }
}
