namespace Microservices.BLL.Models
{
	public class OperationResult<T>
	{
		public T Value { get; set; }
		public string Message { get; set; }
		public bool Error { get; set; }

		public static implicit operator OperationResult<T>(T value) { return new OperationResult<T> { Value = value }; }
		public static implicit operator T(OperationResult<T> result) { return result.Value; }

		public OperationResult<T> Fail(string message, params string[] stringFormats)
		{
			this.Message = string.Format(message, stringFormats);
			this.Error = true;
			return this;
		}
	}
}
