using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using Azure4Alexa.Alexa;
using Azure4Alexa.Sycamore.Constants;
using Azure4Alexa.Sycamore.Data;
using Azure4Alexa.Sycamore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Azure4Alexa.Sycamore.Intents
{
    

    public class Menu
    {
       
        
        public static async Task<SpeechletResponse> GetResults(Session session, Intent intent)        
        {
            var repository = DataFactory.Create(session);
            string filterDateString = intent.Slots["dateFilter"]?.Value;
            DateTime dateFilter = (string.IsNullOrEmpty(filterDateString)) ? DateTime.Now : Convert.ToDateTime(filterDateString);

            var me = await repository.GetMe();
            var menuItem = await repository.GetMenuItem(me.SchoolID, dateFilter);
            
            return AlexaUtils.BuildSpeechletResponse(ParseResults(menuItem, dateFilter), true);

        }

        private static AlexaUtils.SimpleIntentResponse ParseResults(MenuItem menuItem, DateTime dateFilter)
        {
            StringBuilder spokenText = new StringBuilder();
            string dateFilterFormatted = dateFilter.ToString(AlexaConstants.DefaultDateFormat);


            if (menuItem == null)
            {
                if (dateFilter.DayOfWeek == DayOfWeek.Saturday || dateFilter.DayOfWeek == DayOfWeek.Sunday)
                   spokenText.Append(AlexaUtils.AddSentenceTags($"There's nothing on the menu for  {AlexaUtils.AddSayAsTags(dateFilterFormatted, InterpretAs.Date, "mdy")} it's the weekend"));
                else
                    spokenText.Append(AlexaUtils.AddSentenceTags($"There's nothing on the menu for  {AlexaUtils.AddSayAsTags(dateFilterFormatted, InterpretAs.Date, "mdy")}"));
            }
            else
            {
                spokenText.Append(AlexaUtils.AddSentenceTags($"On the menu for  {AlexaUtils.AddSayAsTags(dateFilterFormatted, InterpretAs.Date, "mdy")}"));
                spokenText.Append(AlexaUtils.AddSentenceTagsAndClean(menuItem.MealName));
                spokenText.Append(AlexaUtils.AddSentenceTagsAndClean(menuItem.MealDesc));                
            }

            return new AlexaUtils.SimpleIntentResponse()
            {
                ssmlString = AlexaUtils.AddSpeakTags(spokenText.ToString())                
            };
        }
    }
}