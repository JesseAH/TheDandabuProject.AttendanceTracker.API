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
    public class MessagesController : ApiController
    {
        RestClient _client = new RestClient();
        attendacetracker_dbEntities db = new attendacetracker_dbEntities();

        /// <summary>
        /// Twilio Posts SMS messages here
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual HttpResponseMessage Post([FromBody]TwilioRequest request)
        {
            try
            {
                Dandabu.AttendanceTracker.Models.Message newMessage = new Dandabu.AttendanceTracker.Models.Message();
                newMessage.AccountSid = request.AccountSid;
                newMessage.MessageSid = request.MessageSid;
                newMessage.From = request.From;
                newMessage.To = request.To;
                newMessage.Body = request.Body;
                newMessage.CreatedOn = DateTime.Now;
                newMessage.IsAttendaceData = ParseAttendanceData(newMessage);

                db.Messages.Add(newMessage);

                db.SaveChanges();


                if (newMessage.IsAttendaceData == true)
                    _client.SendMessage("763-316-3318", "952-484-9131", "Accepted attendance data has been sent from " + request.From);
                else
                    _client.SendMessage("763-316-3318", "952-484-9131", "An unknown message has been received from " + request.From);


                return Request.CreateResponse(HttpStatusCode.OK, "Message Received Successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error adding message");
            }
        }

        /// <summary>
        /// Get All Messages sent to our twilio account
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            IList<Message> data = db.Messages.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        /// <summary>
        /// Get Top XX Messages sent to our twilio account
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get(int count)
        {
            IList<Message> data = db.Messages.Take(count).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        private bool ParseAttendanceData(Message newMessage)
        {
            try
            {
                Regex regex = new Regex(@"[Bb](\d{1,3})\s*[Gg](\d{1,3})");
                Match match = regex.Match(newMessage.Body);

                if (match.Success)
                {
                    Attendance_Data newData = new Attendance_Data();
                    newData.Boys = int.Parse(match.Groups[1].Value);
                    newData.Girls = int.Parse(match.Groups[2].Value);
                    newData.Date = DateTime.Now.Date;
                    newData.CreatedOn = DateTime.Now;

                    db.Attendance_Data.Add(newData);
                    db.SaveChanges();
                    return true;
                }


                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
