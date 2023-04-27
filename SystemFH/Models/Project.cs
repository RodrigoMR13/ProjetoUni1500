using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SystemFH.Models
{
    public class Project
    {
        public Project() { }
        [Key]
        public int Id { get; set; }
        [DisplayName("Projeto")]
        public string Name { get; set; }
        [DisplayName("Tipo de Contrato")]
        public TypeTime Type { get; set; }
        [DisplayName("Consultor Nível")]
        public int TypeConsultorId { get; set; }
        [DisplayName("Consultor Nível")]
        public TypeConsultor TypeConsultor { get; set; }
        [DisplayName("Descrição")]
        public string Description { get; set; }
        [DisplayName("Empresa")]
        public string Enterprise { get; set; }
        [DisplayName("Duração")]
        public float Duration { get; set; }
        [DisplayName("Valor")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public float Value { get; set; }
        [DisplayName("Início")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DisplayName("Fim")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        [DisplayName("Situação")]
        public bool Status { get; set; }

        public void AttCalculos()
        {
            if (this.Type == TypeTime.PrecoFechado)
            {
                float tarifa = this.Value / this.Duration;
                this.Value = this.Duration * tarifa;
            }
            else
            {
                this.Value = this.TypeConsultor.Fee * this.Duration;
            }
        }
        public List<TypeConsultor> TypeConsultors { get; set; }
    }
}

public enum TypeTime
{
    Horas,
    PrecoFechado
}
