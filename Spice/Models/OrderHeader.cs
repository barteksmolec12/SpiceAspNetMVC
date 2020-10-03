using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Models
{
	public class OrderHeader
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string userId { get; set; }
		[ForeignKey]
		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}
