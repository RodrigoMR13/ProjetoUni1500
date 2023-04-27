using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SystemFH.Models
{
    public class BankAccount
    {
        public BankAccount() { }
        [Key]
        public int Id { get; set; }
        [DisplayName("Empresa")]
        public int EnterpriseId { get; set; }
        [DisplayName("Empresa")]
        public Enterprise Enterprise { get; set; }
        [DisplayName("Banco")]
        public string Name { get; set; }
        [DisplayName("Saldo Inicial")]
        public float InitialBalance { get; set; }
        [DisplayName("Saldo Atual")]
        public float ActualBalance { get; set; }

        public List<CashManager> CashManagers { get; set; }

        public void AttCalculos()
        {
            this.ActualBalance = 
                this.InitialBalance +
                this.CashManagers.Where(x => x.Type == Type.Recebimento).Sum(x => x.Value) -
                this.CashManagers.Where(x => x.Type == Type.Pagamento).Sum(x => x.Value);
        }
    }
}
