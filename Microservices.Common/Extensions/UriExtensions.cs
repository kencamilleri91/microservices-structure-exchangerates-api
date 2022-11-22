using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Microservices.BLL.Extensions
{
	public static class NameValueCollectionExtensions
	{
		public static string ToUrlQuery(this NameValueCollection query)
			=> "?" + string.Join("&", query.AllKeys.Where(key => !string.IsNullOrWhiteSpace(query[key])).Select(key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(query[key]))));
	}
}
