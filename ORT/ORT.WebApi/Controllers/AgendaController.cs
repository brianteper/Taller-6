﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;

namespace ORT.WebApi.Controllers
{
    public class AgendaController : ApiController
    {
        public HttpResponseMessage GetEvents()
        {
            using (var ortWPContext = new ORTWPEntities())
            {
                var limitDate = DateTime.Today.AddDays(30);

                var agenda = (from a in ortWPContext.Agenda where a.Fecha >= DateTime.Today && a.Fecha < limitDate select a);

                return Request.CreateResponse(HttpStatusCode.OK, agenda.ToArray(), Configuration.Formatters.JsonFormatter);
            }
        }
    }
}