using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using Azure4Alexa.Alexa;
using Azure4Alexa.Sycamore.Data;
using Azure4Alexa.Sycamore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Azure4Alexa.Sycamore.Intents
{
    

    public class MissingAssignments
    {
       
        
        public static async Task<SpeechletResponse> GetResults(Session session, Intent intent)        
        {
            var repository = DataFactory.Create(session);
            //intent.Slots["studentFirstName"]?.Value;

            //return AlexaUtils.BuildSpeechletResponse(simpleIntentResponse, true);
            return AlexaUtils.BuildSpeechletResponse(new AlexaUtils.SimpleIntentResponse() { cardText = AlexaConstants.AppErrorMessage }, true);

        }
    }
}