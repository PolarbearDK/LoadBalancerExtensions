using System;
using LoadBalancerExtensions;
using NUnit.Framework;

namespace UnitTest
{
	[TestFixture]
    public class UnitTests
    {
		[Test]
		[TestCase("http://foo.bar", "", -1, "http://foo.bar")]
		[TestCase("http://foo.bar:88", "", -1, "http://foo.bar:88")]
		[TestCase("http://foo.bar", "http", 80, "http://foo.bar")]
		[TestCase("http://foo.bar", "http", 88, "http://foo.bar:88")]
		[TestCase("http://foo.bar", "https", 443, "https://foo.bar")]
		[TestCase("http://foo.bar", "https", 4000, "https://foo.bar:4000")]
		[TestCase("http://foo.bar:1234", "http", 80, "http://foo.bar")]
		[TestCase("http://foo.bar:1234", "http", 88, "http://foo.bar:88")]
		[TestCase("http://foo.bar:1234", "https", 443, "https://foo.bar")]
		[TestCase("http://foo.bar:1234", "https", 4000, "https://foo.bar:4000")]
	    public void GetOriginalRequestUrlTest(string url, string proto, int port, string expected)
	    {
		    var request = new FakeHttpRequest(url,proto, port);
		    var actual = request.GetOriginalRequestUrl();
			Assert.That(actual, Is.EqualTo(new Uri(expected)));

		}
    }
}
