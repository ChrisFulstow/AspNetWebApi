using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace SelfHostedWebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8080");
            
            var hostConfig = BuildHostConfiguration(baseAddress);

            using (var server = new HttpSelfHostServer(hostConfig))
            using (var webClient = new HttpClient())
            {
                server.OpenAsync().Wait();
                Console.WriteLine("WebAPI server is running at " + baseAddress);

                var response = webClient.GetAsync(new Uri(baseAddress, "/api/process")).Result;
                var processList = response.Content.ReadAsAsync<IEnumerable<ProcessModel>>().Result;
            }

            Console.Read();
        }

        private static HttpSelfHostConfiguration BuildHostConfiguration(Uri baseAddress)
        {
            var configuration = new HttpSelfHostConfiguration(baseAddress);

            configuration.Routes.MapHttpRoute(
                name: "default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            
            return configuration;
        }
    }
}
