using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MongoTest.DAL;
using MongoTest.Models;
using Newtonsoft.Json.Linq;

namespace MongoTest.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        // [HttpGet]
        // public async System.Threading.Tasks.Task<IEnumerable<string>> GetAsync()
        // {
        //     // 
        //     await DataLayer.Find();
        //     return new string[] { "value1", "value2" };
        // }

        // GET api/values/5
        // [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        

        // POST api/values
        // [HttpPost]
        // public void Post([FromBody]string value)
        // {
        // }


        [HttpPost]
        public Applicant Post()
        {
            using (var reader
                = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true))
            {
                string bodyStr = reader.ReadToEnd();
                JObject jObject = JObject.Parse(bodyStr);
                new Applicant().name = jObject["name"].ToString();
                new Applicant().languages = jObject["languages"].ToObject<List<string>>();
                new Applicant().RoleType = jObject["RoleType"].ToObject<Int32>();
            }

            DataLayer dl = new DataLayer();
            dl.Insert(new Applicant());
            //            StringValues contentType;

            return new Applicant();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
