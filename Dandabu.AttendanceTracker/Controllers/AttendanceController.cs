using Dandabu.AttendanceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using Twilio.TwiML;

namespace Dandabu.AttendanceTracker.Controllers
{
    public class AttendanceController : ApiController
    {
        RestClient _client = new RestClient();
        attendacetracker_dbEntities db = new attendacetracker_dbEntities();

        /// <summary>
        /// Gets the messages that have been turned into Attendance Data
        /// </summary>
        /// <returns></returns>
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            IList<Attendance_Data> data = db.Attendance_Data.OrderByDescending(o => o.Date).Take(10).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data.OrderBy(o => o.Date).ToList());
        }

        /// <summary>
        /// Gets the top XX messages that have been turned into Attendance Data
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            IList<Attendance_Data> data = db.Attendance_Data.OrderByDescending(o => o.Date).Take(id).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data.OrderBy(o => o.Date).ToList());
        }

    }
}