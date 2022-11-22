using System;
using System.Text.Json.Serialization;

namespace Microservices.BLL.Models
{
	public class ConvertResponseModel
	{
		[JsonPropertyName("info")]
		public InfoModel Info { get; set; }

		[JsonPropertyName("query")]
		public QueryModel Query { get; set; }

		[JsonPropertyName("date")]
		public DateTime Date { get; set; }

		[JsonPropertyName("historical")]
		public string Historical { get; set; }

		[JsonPropertyName("result")]
		public decimal Result { get; set; }

		[JsonPropertyName("success")]
		public bool Success { get; set; }

		#region Inner Class
		public class InfoModel
		{
			[JsonPropertyName("rate")]
			public decimal Rate { get; set; }

			[JsonPropertyName("timestamp")]
			public long Timestamp { get; set; }
		}
		public class QueryModel
		{
			[JsonPropertyName("amount")]
			public decimal Amount { get; set; }
			
			[JsonPropertyName("from")]
			public string From { get; set; }
			
			[JsonPropertyName("to")]
			public string To { get; set; }
		}
		#endregion
	}
}