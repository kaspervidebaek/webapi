using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using BookApp.ActionFilters;

namespace BookApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new GlobalExceptionAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

        }
    }
}
