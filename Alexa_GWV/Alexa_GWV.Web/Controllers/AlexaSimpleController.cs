using Alexa.Entities;
using Alexa_GWV.Web.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexa_GWV.Web.Controllers
{
    public class AlexaSimpleController : ApiController
    {
        [HttpPost, Route("api/alexa_nicole/demo")]
        public dynamic NicoleSkill(AlexaRequest alexaRequest)
        {
            new Requests().Create(new Request
            {
                Timestamp = alexaRequest.Request.Timestamp,
                Intent = (alexaRequest.Request.Intent == null) ? "" : alexaRequest.Request.Intent.Name,
                AppId = alexaRequest.Session.Application.ApplicationId,
                RequestId = alexaRequest.Request.RequestId,
                SessionId = alexaRequest.Session.SessionId,
                UserId = alexaRequest.Session.User.UserId,
                IsNew = alexaRequest.Session.New,
                Version = "",
                Type = "",
                Reason = "",
                Slots = "",
                DateCreated = DateTime.UtcNow
            });

            return new
            {
                version = "1.0",
                sessionAttributes = new { },
                response = new
                {
                    outputSpeech = new
                    {
                        type = "PlainText",
                        text = "Nicole says that you need to come up with some good ideas for the Alexa Hackathon"
                    },
                    card = new
                    {
                        type = "Simple",
                        title = "Alexa Training",
                        content = "Make sure to attend the Alexa training on 4/12/2017"
                    },
                    shouldEndSession = true
                }
            };
        }
    }
}
