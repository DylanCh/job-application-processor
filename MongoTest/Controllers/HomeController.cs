using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using MongoTest.DAL;
using MongoTest.Models;

namespace MongoTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        [Route(template: "/Home")]
        public IActionResult Index()
        {
            var headers = Request.Headers;
            headers.TryGetValue(key: "Accept", value: out var contentTypes);
            var language = Request.Query["language"].ToString();
            var applicants = (language.Equals(string.Empty) || language==null) 
                ? new DataLayer().FindAll() : new DataLayer().Find(language);
            if (contentTypes.Count != 0)
            {
                foreach (var contentType in contentTypes)
                {
                    if (contentType.ToLower().Contains("xml"))// can be application/xml or text/xml
                    {
                        return base.Content(GetXML(applicants), "application/xml");
                    }
                }
            }
            return base.Json(data: applicants);
        }

        [NonAction]
        private string GetXML(IEnumerable<Applicant> o){
            var sw = new StringWriter();
                XmlTextWriter tw = new XmlTextWriter(sw);
                try{
                    var serializer = new XmlSerializer(o.GetType());
                    serializer.Serialize(tw, o);
                }
                catch{}
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }
    }

    
}
