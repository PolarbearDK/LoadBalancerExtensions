using System;
using System.Web;

namespace LoadBalancerExtensions
{
	/// <summary>
	/// Use these extensions when operating behind a load balancer resulting in Request.Url not containing the correct scheme.
	/// </summary>
	public static class RequestExtensions
	{
		private const string OriginalProtocolHeader = "HTTP_X_FORWARDED_PROTO";

		/// <summary>
		/// Returns true if the request is https or if it was originally a https (secure) request
		/// (but now might be http because it is behind a load-balancer that handles the secure connection)
		/// </summary>
		public static bool OriginalRequestIsSecureConnection(this HttpRequest request)
		{
			return request.IsSecureConnection ||
				   String.Equals(request.ServerVariables[OriginalProtocolHeader], Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Returns true if the request is https or if it was originally a https (secure) request
		/// (but now might be http because it is behind a load-balancer that handles the secure connection)
		/// </summary>
		public static bool OriginalRequestIsSecureConnection(this HttpRequestBase request)
		{
			return request.IsSecureConnection ||
				   String.Equals(request.ServerVariables[OriginalProtocolHeader], Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Returns the protocol originally used by the request. See <see cref="o:OriginalRequestIsSecureConnection"/>.
		/// </summary>
		public static string GetOriginalRequestScheme(this HttpRequest request)
		{
			return request.OriginalRequestIsSecureConnection()
				? Uri.UriSchemeHttps
				: Uri.UriSchemeHttp;
		}

		/// <summary>
		/// Returns the protocol originally used by the request. See <see cref="o:OriginalRequestIsSecureConnection"/>.
		/// </summary>
		public static string GetOriginalRequestScheme(this HttpRequestBase request)
		{
			return request.OriginalRequestIsSecureConnection()
				? Uri.UriSchemeHttps
				: Uri.UriSchemeHttp;
		}

		/// <summary>
		/// Returns the url originally used by the request. />.
		/// </summary>
		public static Uri GetOriginalRequestUrl(this HttpRequest request)
		{
			var scheme = request.ServerVariables[OriginalProtocolHeader];
			if (scheme != null)
			{
				var uri = new UriBuilder(request.Url)
				{
					Scheme = scheme
				};
				return uri.Uri;
			}

			return request.Url;
		}

		/// <summary>
		/// Returns the url originally used by the request. />.
		/// </summary>
		public static Uri GetOriginalRequestUrl(this HttpRequestBase request)
		{
			if (request.Url != null)
			{
				var scheme = request.ServerVariables[OriginalProtocolHeader];
				if (scheme != null)
				{
					var uri = new UriBuilder(request.Url)
					{
						Scheme = scheme
					};
					return uri.Uri;
				}
			}

			return request.Url;
		}
	}
}
