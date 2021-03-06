﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting;
using Rhino.Mocks;

namespace HtmlExtensionsSpecs
{
	[Subject(typeof(HtmlHelperExtensions))]
	public abstract class base_context
	{
		protected static HttpRequestBase _httpRequestBase;
		protected static RequestContext _requestContext;
		protected static RouteData _routeData;
		protected static string _form;
		protected static HtmlHelper _htmlHelper;

		Establish context = () =>
		{
			var builder = new TestControllerBuilder();
			var requestContext = new RequestContext(builder.HttpContext, new RouteData());
			requestContext.HttpContext.Response.Stub(x => x.ApplyAppPathModifier(null)).IgnoreArguments().Do(new Func<string, string>(s => s)).Repeat.Any();

			var viewData = new ViewDataDictionary();

			var viewContext = MockRepository.GenerateStub<ViewContext>();
			viewContext.RequestContext = requestContext;
			viewContext.ViewData = viewData;

			var viewDataContainer = MockRepository.GenerateStub<IViewDataContainer>();
			viewDataContainer.ViewData = viewData;

			_htmlHelper = new HtmlHelper(viewContext, viewDataContainer);
		};
	}

	public class when_generating_a_put_override : base_context
	{
		static MvcHtmlString _tag;

		Because of = () => _tag = _htmlHelper.PutOverrideTag();

		It should_return_a_hidden_field_with_method_put = () => 
			_tag.ToHtmlString().ShouldBe("<input name=\"_method\" type=\"hidden\" value=\"put\" />");
		
	}

	public class when_generating_a_delete_override : base_context
	{
		static MvcHtmlString _tag;

		Because of = () => _tag = _htmlHelper.DeleteOverrideTag();

		It should_return_a_hidden_field_with_method_delete = () => _tag.ToHtmlString().ShouldBe("<input name=\"_method\" type=\"hidden\" value=\"delete\" />");
		
	}
}
