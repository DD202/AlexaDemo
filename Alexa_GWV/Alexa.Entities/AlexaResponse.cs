﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexa.Entities
{
    [JsonObject]
    public class AlexaResponse
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("sessionAttributes")]
        public SessionAttributes Session { get; set; }

        [JsonProperty("response")]
        public ResponseAttributes Response { get; set; }

        public AlexaResponse()
        {
            Version = "1.0";
            Session = new SessionAttributes();
            Response = new ResponseAttributes();
        }

        public AlexaResponse(string outputSpeechText)
            : this()
        {
            Response.OutputSpeech.Text = outputSpeechText;
            Response.Card.Content = outputSpeechText;
        }

        public AlexaResponse(string outputSpeechText, bool shouldEndSession)
            : this()
        {
            Response.OutputSpeech.Text = outputSpeechText;
            Response.ShouldEndSession = shouldEndSession;

            if (shouldEndSession)
            {
                Response.Card.Content = outputSpeechText;
            }
            else
            {
                Response.Card = null;
            }
        }

        public AlexaResponse(string outputSpeechText, string cardContent)
            : this()
        {
            Response.OutputSpeech.Text = outputSpeechText;
            Response.Card.Content = cardContent;
        }

        [JsonObject("sessionAttributes")]
        public class SessionAttributes
        {
            [JsonProperty("memberId")]
            public int MemberId { get; set; }
        }

        [JsonObject("response")]
        public class ResponseAttributes
        {
            [JsonProperty("shouldEndSession")]
            public bool ShouldEndSession { get; set; }

            [JsonProperty("outputSpeech")]
            public OutputSpeechAttributes OutputSpeech { get; set; }

            [JsonProperty("card")]
            public CardAttributes Card { get; set; }

            [JsonProperty("reprompt")]
            public RepromptAttributes Reprompt { get; set; }

            public ResponseAttributes()
            {
                ShouldEndSession = true;
                OutputSpeech = new OutputSpeechAttributes();
                Card = new CardAttributes();
                Reprompt = new RepromptAttributes();
            }

            [JsonObject("outputSpeech")]
            public class OutputSpeechAttributes
            {
                [JsonProperty("type")]
                public string Type { get; set; }

                [JsonProperty("text")]
                public string Text { get; set; }

                [JsonProperty("ssml")]
                public string Ssml { get; set; }

                public OutputSpeechAttributes()
                {
                    Type = "PlainText";
                }
            }

            public const string CARD_TYPE_SIMPLE = "Simple";
            public const string CARD_TYPE_STANDARD = "Standard";
            public const string CARD_TYPE_LINK_ACCOUNT = "LinkAccount";

            [JsonObject("card")]
            public class CardAttributes
            {
                [JsonProperty("type")]
                public string Type { get; set; }

                [JsonProperty("title")]
                public string Title { get; set; }

                [JsonProperty("content")]
                public string Content { get; set; }

                [JsonProperty("text")]
                public string Text { get; set; }

                [JsonProperty("image")]
                public ImageAttributes image { get; set; }


                public CardAttributes()
                {
                    Type = "Simple";
                    image = new ImageAttributes();

                }
            }

            [JsonObject("image")]
            public class ImageAttributes
            {
                [JsonProperty("smallImageUrl")]
                public string SmallImageUrl { get; set; }

                [JsonProperty("largeImageUrl")]
                public string LargeImageUrl { get; set; }
            }

            [JsonObject("reprompt")]
            public class RepromptAttributes
            {
                [JsonProperty("outputSpeech")]
                public OutputSpeechAttributes OutputSpeech { get; set; }

                public RepromptAttributes()
                {
                    OutputSpeech = new OutputSpeechAttributes();
                }
            }
        }

        public void HelpIntentHandler(Request request)
        {
            throw new NotImplementedException();
        }
    }
}