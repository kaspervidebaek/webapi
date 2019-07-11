using BookApp.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;

namespace BookApp {
    public class WebApiApplication : System.Web.HttpApplication {
        private static IUnityContainer _container;

        private static IUnityContainer CreateUnityContainer() {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        private static void RegisterTypes(UnityContainer container) {
            Implementation.TypeRegistrations.RegisterType(container);
            Repository.TypeRegistrations.RegisterType(container);
        }

        private static void RegisterIoC() {
            if (_container == null) {
                _container = CreateUnityContainer();
            }
        }

        protected void Application_Start() {
            RegisterIoC();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register); 
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(_container);
            var formatters = System.Web.Http.GlobalConfiguration.Configuration.Formatters;
            var appXmlType = formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

        }
    }
}
