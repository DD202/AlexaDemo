using Alexa.Entities;
using Alexa_GWV.Web.DataContexts;
using Alexa_GWV.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Alexa_GWV.Web.Controllers
{
    public class AlexaController : ApiController
    {

        [HttpPost, Route("api/alexa/dotnetskill")]
        public async Task<dynamic> AlexaAnonymousEndpoint(AlexaRequest alexaRequest)
        {
            var requestMessage = new Requests().Create(ParseRequestMessage(alexaRequest));

            AlexaResponse response = null;
            switch (requestMessage.Type)
            {
                case "LaunchRequest":
                    response = await LaunchRequestHandler(requestMessage);
                    break;
                case "IntentRequest":
                    response = await IntentRequestHandler(requestMessage);
                    break;
                case "SessionEndedRequest":
                    response = SessionEndedRequestHandler(requestMessage);
                    break;
            }


            return response;
        }

        private async Task<AlexaResponse> LaunchRequestHandler(Request requestMessage)
        {
            var response = new AlexaResponse();
            response.Response.OutputSpeech.Text = "Welcome to the dotnet Alexa Skils Framework. You can ask me to tell you a random fact, send you a random picture, or tell you something personal.";
            response.Response.Card.Title = "Welcome to the .Net Alexa Skils Framework";
            response.Response.Card.Content = "You can ask me to tell you a random fact, send you a random picture, or tell you something personal.";
            response.Response.ShouldEndSession = false;

            return response;
        }

        private async Task<AlexaResponse> IntentRequestHandler(Request requestMessage)
        {
            AlexaResponse response = null;

            switch (requestMessage.Intent)
            {
                case "RandomFactIntent":
                    response = await RandomFactIntent();
                    break;
                case "RandomImageIntent":
                    response = await RandomImageIntent();
                    break;
                case "PersonalInfoIntent":
                    response = await PersonalInfoIntent(requestMessage);
                    break;
                case "AMAZON.CancelIntent":
                case "AMAZON.StopIntent":
                    response = CancelOrStopIntentHandler(requestMessage);
                    break;
                case "AMAZON.HelpIntent":
                    response = HelpIntentHandler(requestMessage);
                    break;
            }
            return response;
        }

        private async Task<AlexaResponse> RandomFactIntent()
        {
            var randomFact = string.Empty;
            using (var db = new ApplicationDbContext())
            {
                randomFact = db.InformationalFacts.OrderBy(x => Guid.NewGuid()).FirstOrDefault().Fact;
            }

            var response = new AlexaResponse();
            response.Response.OutputSpeech.Text = $"Did you know that {randomFact}";
            response.Response.Card.Title = ".Net Alexa Skills Framework - Random Fact";
            response.Response.Card.Content = $"Did you know that {randomFact}";
            response.Response.ShouldEndSession = true;

            return response;
        }

        private async Task<AlexaResponse> RandomImageIntent()
        {
            var randomPicture = new Picture();
            using (var db = new ApplicationDbContext())
            {
                randomPicture = db.Pictures.OrderBy(x => Guid.NewGuid()).First();
            }

            var response = new AlexaResponse();
            response.Response.OutputSpeech.Text = $"{randomPicture.Description} - picture shown, check your alexa app";
            response.Response.Card.Title = ".Net Alexa Skills Framework - Random Picture";
            response.Response.Card.Text = $"{randomPicture.Description}";
            response.Response.Card.image.SmallImageUrl = randomPicture.SmallImageUrl;
            response.Response.Card.image.LargeImageUrl = randomPicture.LargeImageUrl;
            response.Response.Card.Type = AlexaResponse.ResponseAttributes.CARD_TYPE_STANDARD;
            response.Response.ShouldEndSession = true;

            return response;
        }

        private async Task<AlexaResponse> PersonalInfoIntent(Request requestMessage)
        {
            var response = new AlexaResponse();

            if (User.Identity.IsAuthenticated)
            {
                var preferences = GetLoggedInUserPreferences();
                var name = preferences.PreferredName;
                var weatherReport = await GetWeatherReport(preferences.City);
                response.Response.OutputSpeech.Text = $"Hello {name}, {weatherReport}";
                response.Response.Card.Title = ".Net Skills Framework - Weather";
                response.Response.Card.Content = weatherReport;
                response.Response.ShouldEndSession = true;
            }
            else
            {
                //Add account 'linking card' to display properly
                response.Response.OutputSpeech.Text = "You must have a linked account to access this functionality. Please visit the app to complete the account setup process.";
                response.Response.Card.Type = AlexaResponse.ResponseAttributes.CARD_TYPE_LINK_ACCOUNT;
                response.Response.ShouldEndSession = true;

            }
            return response;
        }

        private async Task<string> GetWeatherReport(string city)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUri = $"http://weathers.co/api.php?city={city}";
                Uri uri = new Uri(requestUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var result = await client.GetStringAsync(uri);
                var weatherResponse = JObject.Parse(result);
                var temp = weatherResponse["data"]["temperature"].ToString();
                var forecast = weatherResponse["data"]["skytext"].ToString();
                var location = weatherResponse["data"]["location"].ToString();
                double tempC;
                double.TryParse(temp, out tempC);

                double tempF = ((9.0 / 5.0) * tempC) + 32;

                return $"today in {location} it is {tempF} degrees fahrenheit and the forecast calls for {forecast}.";
            }
        }

        private UserPreference GetLoggedInUserPreferences()
        {
            using (var db = new ApplicationDbContext())
            {
                var loggedInUser = User.Identity.GetUserId();
                return db.UserPreferences
                    .Where(x => x.UserId == loggedInUser)
                    .FirstOrDefault();
            }
        }

        private AlexaResponse CancelOrStopIntentHandler(Request requestMessage)
        {
            var response = new AlexaResponse();
            response.Response.OutputSpeech.Text = "Goodbye";
            response.Response.Card.Title = ".Net Skills Framework";
            response.Response.Card.Content = "Come back soon!";
            response.Response.ShouldEndSession = true;

            return response;
        }

        private AlexaResponse HelpIntentHandler(Request requestMessage)
        {
            var response = new AlexaResponse();
            response.Response.OutputSpeech.Text = "To use the dotnet Alexa Skils Framework, you can say things like...";
            response.Response.Card.Title = ".Net Alexa Skills Framework";
            response.Response.Card.Content = "";
            response.Response.ShouldEndSession = false;

            return response;

        }

        private AlexaResponse SessionEndedRequestHandler(Request requestMessage)
        {
            //Clean up any serverside stuff here if needed
            return null;
        }

        private Request ParseRequestMessage(AlexaRequest alexaRequest)
        {
            return new Request()
            {
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
                DateCreated = DateTime.UtcNow,
                AccessToken = alexaRequest.Session.User.AccessToken
            };
        }
    }
}
