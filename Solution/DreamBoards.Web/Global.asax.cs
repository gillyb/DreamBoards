using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using CommonGround;
using CommonGround.Logging;
using CommonGround.MvcInvocation;
using DreamBoards.Domain.Settings;
using DreamBoards.Web.Filters;
using PlatformClient.Platform;

namespace MiamiApp.Web
{
	public class Global : System.Web.HttpApplication
	{
		private ICommonContainer _container;

		protected void Application_Start(object sender, EventArgs e)
		{
			var currentAssembly = typeof(MyCanvasAppActionInvoker).Assembly;
			var domainAssemblies = new[]
			                       	{
			                       		typeof (IApplicationSettings).Assembly,
			                       		typeof (IPlatformConfiguration).Assembly,
										currentAssembly
			                       	};

			// IOC
			_container = new CommonContainer();
			_container.RegisterTypes(new Dictionary<Type, Type> { { typeof(IActionInvoker), typeof(MyCanvasAppActionInvoker) } });

			var log4NetConfigurator = new Log4NetConfigurator { Container = _container };
			log4NetConfigurator.Configure();

			_container.AutoWire(domainAssemblies);

			RoutesRegistrar.RegisterRoutes(RouteTable.Routes, new[] { currentAssembly });
			var controllerTypes = RoutesRegistrar.GetControllerTypes(new[] { currentAssembly }).ToArray();
			_container.RegisterTransients(controllerTypes);
			var controllerFactory = new WindsorControllerFactory(_container);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);


			// Events
			_container.AutoWireEvents(domainAssemblies);


		}

	}
}