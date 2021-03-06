﻿using System.Web.Routing;

namespace RestfulRouting
{
	public abstract class Mapper
	{
		private readonly IRouteHandler _routeHandler;

		protected Mapper(IRouteHandler routeHandler)
		{
			_routeHandler = routeHandler;
		}

		protected Route GenerateRoute(string path, string controller, string action, string[] httpMethods)
		{
			return new Route(path,
				new RouteValueDictionary(new { controller, action }),
				new RouteValueDictionary(new { httpMethod = new RestfulHttpMethodConstraint(httpMethods) }),
				_routeHandler);
		}
	}
}
