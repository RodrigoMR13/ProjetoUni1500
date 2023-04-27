using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SystemFH.Models
{
    public class CashManager
    {
        public CashManager() { }
        [Key]
        public int Id { get; set; }
        [DisplayName("Data de Realização")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RealizationDate { get; set; }
        [DisplayName("Data de Caixa")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CashDate { get; set; }
        [DisplayName("Mês de Caixa")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/yyyy}")]
        public DateTime CashMonth { get; set; }
        [DisplayName("Mês de Competência")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/yyyy}")]
        public DateTime CompetMonth { get; set; }
        [DisplayName("Mês Contábil")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/yyyy}")]
        public DateTime AccMonth { get; set; }
        [DisplayName("Empresa")]
        public int? EnterpriseId { get; set; }
        [DisplayName("Empresa")]
        public Enterprise Enterprise { get; set; }
        [DisplayName("Centro de Custo")]
        public int? CostCenterId { get; set; }
        [DisplayName("Centro de Custo")]
        public CostCenter CostCenter { get; set; }
        [DisplayName("Conta")]
        public int? AccountId { get; set;}
        [DisplayName("Conta")]
        public Account Account { get; set; }
        [DisplayName("Nome")]
        public int? PersonId { get; set; }
        [DisplayName("Nome")]
        public Person Person { get; set; }
        [DisplayName("Banco")]
        public int? BankAccountId { get; set; }
        [DisplayName("Banco")]
        public BankAccount BankAccount { get; set;}
        [DisplayName("Categoria")]
        public int? CategoryId { get; set; }
        [DisplayName("Categoria")]
        public Category Category { get; set; }
        [DisplayName("Descrição")]
        public string Description { get; set; }
        [DisplayName("Valor")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public float Value { get; set; }
        [DisplayName("Pago")]
        public bool Comp { get; set; }
        [DisplayName("Saldo do Banco")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public float BankBalance { get; set; }
        [DisplayName("Saldo da Empresa")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public float EnterpriseBalance { get; set; }
        public Type Type { get; set; }

        public void AttCalculos()
        {
            this.CashMonth = this.CashDate;
            this.AccMonth = this.CompetMonth;
        }
    }

    public enum Type
    {
        Recebimento,
        Pagamento
    }
}
