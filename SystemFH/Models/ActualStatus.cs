using SystemFH.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SystemFH.Models
{
    public enum TypeObject
    {
        GESTAO,
        ETL,
        DASH,
        BBP,
        AULA,
        PREPAULA,
        DEV,
        MANUT
    }
    public class ActualStatus
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Turma/Círculo")]
        public int CircleId { get; set; }
        [DisplayName("Turma/Círculo")]
        public Circle Circle { get; set; }
        [DisplayName("Projeto")]
        public int ProjectId { get; set; }
        [DisplayName("Projeto")]
        public Project Project { get; set; }
        [DisplayName("Tipo")]
        public TypeObject TypeObject { get; set; }
        [DisplayName("Consultor Nível")]
        public int TypeConsultorId { get; set; }
        [DisplayName("Consultor Nível")]
        public TypeConsultor TypeConsultor { get; set; }
        public string Description { get; set; }
        public float TimePlanned { get; set; }
        [DisplayName("Valor")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public float Value { get; set; }
        [DisplayName("Consultor")]
        public int PersonId { get; set; }
        [DisplayName("Consultor")]
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        [DisplayName("Tempo Utilizado")]
        public float RealTime { get; set; }
        [DisplayName("Entregue")]
        public bool Delivered { get; set; }
        [DisplayName("Produtividade")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public float Productivity { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM}")]
        public DateTime Sprint { get; set; }
        [DisplayName("Valor Final")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public float FinalValue { get; set; }
        [DisplayName("Inserido Por:")]
        public int UserId { get; set; }

        public List<DayTime> DayTimes { get; set; }

        public void AttCalculos()
        {
            if (this.DayTimes != null && this.DayTimes.Count > 0)
            {
                this.RealTime = this.DayTimes.Sum(x => x.RealTime);
                this.Delivered = this.DayTimes.FirstOrDefault(x => x.Delivered == true) != null;
            }

            if (this.Project.Type == TypeTime.PrecoFechado)
            {
                float tarifa = this.Project.Value / this.Project.Duration;
                this.Value = this.TimePlanned * tarifa;
            }
            else
            {
                this.Value = this.TypeConsultor.Fee * this.TimePlanned;
            }

            if (this.Delivered)
            {
                this.Productivity = this.TimePlanned / this.RealTime;

                if (this.Project.Type == TypeTime.PrecoFechado)
                {
                    float tarifa = this.Project.Value / this.Project.Duration;
                    this.FinalValue = this.TimePlanned * tarifa;
                }
                else
                {
                    this.FinalValue = this.TypeConsultor.Fee * this.TimePlanned;
                }
            }
        }

        public List<TypeConsultor> TypeConsultors { get; set; }
    }
}
