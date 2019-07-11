using BookApp.Unity;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
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

            // KV: Added global formatter for all api returns

            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
            json.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(formatter);

        }
    }
}
