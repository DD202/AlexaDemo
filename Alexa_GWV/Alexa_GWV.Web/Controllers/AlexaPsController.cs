using Alexa.Entities;
using Alexa_GWV.Web.DataContexts;
using Alexa_GWV.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Alexa_GWV.Web.Controllers
{
    public class AlexaPsController : ApiController
    {
        private const string ApplicationId = "amzn1.ask.skill.0fde26e8-a890-4a92-9dd5-b6d8305fd69e";
        //Add check to throw httprequesterror....
        [HttpPost, Route("api/alexa-ps/demo")]
        public dynamic Pluralsight(AlexaRequest alexaRequest)
        {
            //if (alexaRequest.Session.Application.ApplicationId != ApplicationId)
            //{
            //    throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));
            //}


            //var totalSeconds = (DateTime.UtcNow - alexaRequest.Request.Timestamp).TotalSeconds;
            //if (totalSeconds <= 0 || totalSeconds > 150)
            //{
            //    throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));
            //}




            var request = new Requests().Create(new Request
            {
                MemberId = alexaRequest.Session.Attributes.MemberId,
                Timestamp = alexaRequest.Request.Timestamp,
                Intent = (alexaRequest.Request.Intent == null) ? "" : alexaRequest.Request.Intent.Name,
                AppId = alexaRequest.Session.Application.ApplicationId,
                RequestId = alexaRequest.Request.RequestId,
                SessionId = alexaRequest.Session.SessionId,
                UserId = alexaRequest.Session.User.UserId,
                IsNew = alexaRequest.Session.New,
                Version = (alexaRequest.Version == null) ? "" : alexaRequest.Version,
                Type = (alexaRequest.Request.Type == null) ? "" : alexaRequest.Request.Type,
                Reason = (alexaRequest.Request.Reason == null) ? "" : alexaRequest.Request.Reason,
                SlotsList = alexaRequest.Request.Intent.GetSlots(),
                DateCreated = DateTime.UtcNow
            });

            AlexaResponse response = null;
            switch (request.Type)
            {
                case "LaunchRequest":
                    response = LaunchRequestHandler(request);
                    break;
                case "IntentRequest":
                    response = IntentRequestHandler(request);
                    break;
                case "SessionEndedRequest":
                    response = SessionEndedRequestHandler(request);
                    break;
            }

            if (request.Intent == "AMAZON.NoIntent")
            {
                response.Response.OutputSpeech.Text = "OK, I'll be quiet now. Have a nice day!";
                response.Response.ShouldEndSession = true;
            }
            return response;

        }

        private AlexaResponse LaunchRequestHandler(Request request)
        {
            var response = new AlexaResponse("Welcome to Pluralsight. What would you like to hear, the Top Courses or New Courses?");
            response.Session.MemberId = request.MemberId;
            response.Response.Card.Title = "Pluralsight";
            response.Response.Card.Content = "";
            response.Response.Reprompt.OutputSpeech.Text = "Please pick one, Top Courses or New Courses?";
            response.Response.ShouldEndSession = false;
            return response;
        }

        private AlexaResponse IntentRequestHandler(Request request)
        {
            AlexaResponse response = null;

            switch (request.Intent)
            {
                case "NewCoursesIntent":
                    response = NewCoursesIntentHandler(request);
                    break;
                case "TopCoursesIntent":
                    response = TopCoursesIntentHandler(request);
                    break;
                case "AMAZON.CancelIntent":
                case "AMAZON.StopIntent":
                    response = CancelOrStopIntentHandler(request);
                    break;
                case "AMAZON.HelpIntent":
                    response = HelpIntentHandler(request);
                    break;
            }
            return response;
        }

        private AlexaResponse NewCoursesIntentHandler(Request request)
        {
            var output = new StringBuilder("Here are the latest courses. ");

            using (var db = new ApplicationDbContext())
            {
                db.Courses.Take(10).OrderByDescending(c => c.DateCreated).ToList()
                    .ForEach(c => output.AppendFormat("{0} by {1}", c.Title, c.Author));

            }
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse TopCoursesIntentHandler(Request request)
        {
            int limit = 10;
            var criteria = string.Empty;
            if (request.SlotsList.Any())
            {
                int maxLimit = 10;
                var limitValue = request.SlotsList.First(x => x.Key == "Limit").Value;

                if (!string.IsNullOrWhiteSpace(limitValue) && int.TryParse(limitValue, out limit) && !(limit >= 1 && limit <= maxLimit))
                {
                    limit = maxLimit;

                }

                criteria = request.SlotsList.FirstOrDefault(x => x.Key.Equals("criteria", StringComparison.InvariantCultureIgnoreCase)).Value;
            }

            var output = new StringBuilder();
            output.AppendFormat("Here are the top {0} {1}. ", limit, string.IsNullOrWhiteSpace(criteria) ? "courses" : criteria);


            using (var db = new ApplicationDbContext())
            {
                if (criteria.Equals("authors", StringComparison.InvariantCultureIgnoreCase))
                {
                    db.Courses.Take(limit).OrderByDescending(c => c.Votes).ToList()
                        .ForEach(c => output.AppendFormat("{0} ", c.Author));
                }
                else
                {
                    db.Courses.Take(limit).OrderByDescending(c => c.Votes).ToList()
                   .ForEach(c => output.AppendFormat("{0} by {1}", c.Title, c.Author));
                }

            }
            return new AlexaResponse(output.ToString());
        }

        private AlexaResponse CancelOrStopIntentHandler(Request request)
        {
            return new AlexaResponse("Thanks for listening, let's talk again soon.", true);
        }

        private AlexaResponse HelpIntentHandler(Request request)
        {
            var response = new AlexaResponse("To use the plural sight skill, you can say lots of things that i don't feel like typing", false);
            response.Response.Reprompt.OutputSpeech.Text = "Please select one, top courses or new courses";
            return response;
        }

        private AlexaResponse SessionEndedRequestHandler(Request request)
        {
            //use to clean up server side session if needed
            return null;
        }
    }
}
