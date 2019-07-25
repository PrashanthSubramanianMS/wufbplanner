using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using wufbdeferplan.Models;

namespace wufbdeferplan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class wufbplanController : ControllerBase
    {
        static List<WindowsRelease> releases = new List<WindowsRelease>
        {
            new WindowsRelease { Version  = 1607, Channel = "SAC", BornOfRelese = new DateTime(2016,8,2), EndServiceDate=new DateTime(2019,5,1)},
            new WindowsRelease { Version  = 1703, Channel = "CB", BornOfRelese = new DateTime(2017,4,11), EndServiceDate=new DateTime(2019,10,8)},
            new WindowsRelease { Version  = 1703, Channel = "SAC", BornOfRelese = new DateTime(2017,7,11), EndServiceDate=new DateTime(2019,10,8)},
            new WindowsRelease { Version  = 1709, Channel = "SACT", BornOfRelese = new DateTime(2017,10,17), EndServiceDate=new DateTime(2020,4,14)},
            new WindowsRelease { Version  = 1709, Channel = "SAC", BornOfRelese = new DateTime(2018,1,18), EndServiceDate=new DateTime(2020,4,14)},
            new WindowsRelease { Version  = 1803, Channel = "SACT", BornOfRelese = new DateTime(2018,4,30), EndServiceDate=new DateTime(2020,11,10)},
            new WindowsRelease { Version  = 1803, Channel = "SAC", BornOfRelese = new DateTime(2018,7,10), EndServiceDate=new DateTime(2020,11,10)},
            new WindowsRelease { Version  = 1809, Channel = "SACT", BornOfRelese = new DateTime(2018,11,13), EndServiceDate=new DateTime(2021,5,11)},
            new WindowsRelease { Version  = 1809, Channel = "SAC", BornOfRelese = new DateTime(2019,3,28), EndServiceDate=new DateTime(2021,5,11)},
            new WindowsRelease { Version  = 1903, Channel = "SAC", BornOfRelese = new DateTime(2019,5,21), EndServiceDate=new DateTime(2020,12,8)}
        };

        // GET api/values
        [HttpGet]
        public ActionResult<List<WindowsRelease>> Get()
        {
            return releases;
        }

        // GET Applicable Versions
        [HttpGet("{deployDate}")]
        public ActionResult<IEnumerable<int>> Get(DateTime deployDate)
        {
            List<int> applicableVersions = new List<int>();
            foreach (WindowsRelease wr in releases)
            {
                if (wr.BornOfRelese<deployDate && wr.EndServiceDate>=deployDate) applicableVersions.Add(wr.Version);
            }
            return applicableVersions;
        }

        // GET DeferalDays
        [HttpGet("{deployDate,version}")]
        public ActionResult<int> Get(DateTime deployDate, int version)
        {
            int deferDays = -1;
            foreach (WindowsRelease wr in releases)
            {
                if (wr.Version== version && wr.EndServiceDate>= deployDate) deferDays = (int)(deployDate-wr.BornOfRelese).TotalDays;
            }
            return deferDays;
        }

        // GET DeployDate
        [HttpGet("{deferDays, channel}")]
        public ActionResult<DateTime> Get(int deferDays, string channel = "SAC")
        {
            DateTime deployDate = DateTime.UtcNow;
            foreach (WindowsRelease wr in releases)
            {
                if ( wr.BornOfRelese<= DateTime.Now && wr.Channel == channel) deployDate  = wr.BornOfRelese + new TimeSpan(deferDays,0,0,0) ;
            }
            return deployDate;
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<List<WindowsRelease>> Get(int id)
        {
            return releases.Where(r=>r.Version==id).ToList();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
