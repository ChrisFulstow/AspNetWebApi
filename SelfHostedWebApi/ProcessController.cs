using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Web.Http;

namespace SelfHostedWebApi
{
    public class ProcessController : ApiController
    {
        public IEnumerable<ProcessModel> GetAll()
        {
            var processes = Process.GetProcesses();
            var processList = processes.Select(p =>
                new ProcessModel { Id = p.Id, Name = p.ProcessName });

            return processList.ToList();
        }

        public ProcessModel GetById(int id)
        {
            try
            {
                var process = Process.GetProcessById(id);
                return new ProcessModel { Id = process.Id, Name = process.ProcessName };
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
