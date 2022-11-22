using System;

namespace Microservices.BLL.Models
{
	public class BaseEntity
	{
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }
		
		public DateTime? UpdatedAt { get; set; } // TODO Override default DbContext behaviour to automatically update these fields on insert/update/delete
	}
}