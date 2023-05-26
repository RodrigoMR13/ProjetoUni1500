using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemFH.Models
{
    public class Theme
    {

		[Key]
		public int Id { get; set; }
		[DisplayName("Tema")]
		public string Name { get; set; }
        [DisplayName("Descrição")]
        public string Description { get; set; } = string.Empty;

        [DisplayName("Inserido Por:")]
        public int UserId { get; set; }

        public List<Learning> Learnings { get; set; }
	}
}
