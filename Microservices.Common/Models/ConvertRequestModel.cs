namespace Microservices.BLL.Models
{
	public class ConvertRequestModel
	{
		public decimal Amount { get; set; }
		public string ToCurrency { get; set; }
		public string FromCurrency { get; set; }
	}
}