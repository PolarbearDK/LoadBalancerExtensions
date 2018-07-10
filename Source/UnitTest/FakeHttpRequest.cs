using System;
using System.Collections.Specialized;
using System.Web;

namespace UnitTest
{
	public class FakeHttpRequest: HttpRequestBase
	{
		private const string OriginalProtocolHeader = "HTTP_X_FORWARDED_PROTO";
		private const string OriginalPortHeader = "HTTP_X_FORWARDED_PORT";

		public override Uri Url { get; }
		public override NameValueCollection ServerVariables { get; }

		public FakeHttpRequest(string url, string proto, int port)
		{
			Url = new Uri(url);

			var nameValueCollection = new NameValueCollection();
			if(!string.IsNullOrEmpty(proto))
				nameValueCollection.Add(OriginalProtocolHeader, proto);
			if(port != -1)
				nameValueCollection.Add(OriginalPortHeader, port.ToString());

			ServerVariables = nameValueCollection;
		}
	}
}
