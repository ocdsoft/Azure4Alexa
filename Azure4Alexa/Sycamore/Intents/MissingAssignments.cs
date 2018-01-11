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
    

    public class MissingAssignments
    {
       
        
        public static async Task<SpeechletResponse> GetResults(Session session, Intent intent)        
        {
            var repository = DataFactory.Create(session);
            var studentFirstName = intent.Slots["studentFirstName"]?.Value.ToProperCase();
            string dateFilterString = intent.Slots["date"]?.Value;
            DateTime dateFilter = (string.IsNullOrEmpty(dateFilterString)) ? DateTime.Now.AddDays(-7) : Convert.ToDateTime(dateFilterString);

            var me = await repository.GetMe();
            var student = await repository.GetStudent(me.FamilyID, studentFirstName);
            var missingAssignments = await repository.GetMissingAssingments(student.ID, dateFilter);
            
            return AlexaUtils.BuildSpeechletResponse(ParseResults(missingAssignments.ToList(),student,me), true);

        }

        private static AlexaUtils.SimpleIntentResponse ParseResults(List<MissingAssignment> missingAssignments, Student student, Me me)
        {
            StringBuilder spokenText = new StringBuilder();
            int count = (missingAssignments == null) ? 0 : missingAssignments.Count;

            if (count > 0)
            {
                string plural = (count > 1) ? "s" : "";
                spokenText.Append(AlexaUtils.AddSentenceTags($"{student.FirstName} has {count} missing assignment{plural}"));
            }
            else
            {
                spokenText.Append(AlexaUtils.AddSentenceTags($"{student.FirstName} has no missing assignments"));
                spokenText.Append(AlexaUtils.AddSentenceTags(AlexaUtils.AddSayAsTags($"Great job, {student.FirstName}", InterpretAs.Interjection)));
            }

            if (missingAssignments != null)
            {
                foreach (var ma in missingAssignments)
                {
                    spokenText.Append(AlexaUtils.AddSentenceTagsAndClean(ma.ClassName));
                    spokenText.Append(AlexaUtils.AddSentenceTagsAndClean(ma.Title));
                    spokenText.Append(AlexaUtils.AddSentenceTagsAndClean(ma.Description));
                    spokenText.Append(AlexaUtils.AddSentenceTags("<emphasis level='strong'>Due date</emphasis> " + AlexaUtils.AddSayAsTags(ma.DueDateFormatted, InterpretAs.Date, "mdy")));
                }
            }


            return new AlexaUtils.SimpleIntentResponse()
            {
                ssmlString = AlexaUtils.AddSpeakTags(spokenText.ToString()),
                largeImage = string.Format(Url.StudentPhoto, me.SchoolID, student.Picture),
                smallImage = Url.Logo
            };
        }
    }
}