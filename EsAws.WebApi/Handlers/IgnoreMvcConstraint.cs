using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace EsAws.WebApi.Handlers
{
    public class IgnoreMvcConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var isStreamUpload = route.Url.ToLower().Contains("StreamUpload");

            return !isStreamUpload;
        }
    }
}