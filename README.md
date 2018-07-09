# LoadBalancerExtensions.
Extension metods to request object to get original request url or scheme when operating behind a load balancer.
Methods fallback to original properties when not behind a load balancer. 

## Adds the following extensions to request object:
* request.GetOriginalRequestScheme() to get scheme (http or https).
* request.GetOriginalRequestUrl() to get original url.
* request.OriginalRequestIsSecureConnection() like IsSecureConnection property.
