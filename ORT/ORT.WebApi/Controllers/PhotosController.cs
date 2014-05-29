using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;

namespace ORT.WebApi.Controllers
{
    public class PhotosController : ApiController
    {
        public HttpResponseMessage GetPhotos()
        {
            using (var ortWPContext = new ORTWPEntities())
            {
                var photos = (from p in ortWPContext.Fotos where p.Vigente == true select p).Take(1);

                return Request.CreateResponse(HttpStatusCode.OK, photos.ToArray(), Configuration.Formatters.JsonFormatter);
            }
        }
    }
}