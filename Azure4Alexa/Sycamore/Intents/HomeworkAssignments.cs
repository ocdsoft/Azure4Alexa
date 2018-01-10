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
    

    public class HomeworkAssignments
    {
       
        
        public static async Task<SpeechletResponse> GetResults(Session session, Intent intent)        
        {
            var repository = DataFactory.Create(session);
            var studentFirstName = intent.Slots["studentFirstName"]?.Value.ToProperCase();
            string filterDateString = intent.Slots["dateFilter"]?.Value;
            DateTime dateFilter = (string.IsNullOrEmpty(filterDateString)) ? DateTime.Now : Convert.ToDateTime(filterDateString);

            var me = await repository.GetMe();
            var student = await repository.GetStudent(me.FamilyID, studentFirstName);
            var homeworkAssignments = await repository.GetHomeworkAssingments(student.ID, dateFilter);
            
            return AlexaUtils.BuildSpeechletResponse(ParseResults(homeworkAssignments,student,me, dateFilter), true);

        }

        private static AlexaUtils.SimpleIntentResponse ParseResults(List<HomeworkAssignment> homeworkAssignments, Student student, Me me, DateTime dateFilter)
        {
            StringBuilder spokenText = new StringBuilder();
            int count = (homeworkAssignments == null) ? 0 : homeworkAssignments.Count;

            if (count > 0)
            {
                string plural = (count > 1) ? "s" : "";
                spokenText.Append(AlexaUtils.AddSentenceTags($"Yes, {student.FirstName} has {count} homework assignment{plural}"));
            }
            else
            {                
                spokenText.Append(AlexaUtils.AddSentenceTags($"{student.FirstName} has no homework due on {AlexaUtils.AddSayAsTags(Convert.ToDateTime(dateFilter).ToString("yyyyMMdd"), InterpretAs.Date, "mdy")}"));
                spokenText.Append(AlexaUtils.AddSentenceTags(AlexaUtils.AddSayAsTags($"Yipee", InterpretAs.Interjection)));
            }

            if (homeworkAssignments != null)
            {
                foreach (var ma in homeworkAssignments)
                {
                    spokenText.Append(AlexaUtils.AddSentenceTagsAndClean(ma.ClassName));
                    spokenText.Append(AlexaUtils.AddSentenceTagsAndClean(ma.Title));
                    spokenText.Append(AlexaUtils.AddSentenceTagsAndClean(ma.Description));
                    spokenText.Append(AlexaUtils.AddSentenceTags("<emphasis level='strong'>Due date</emphasis> " + AlexaUtils.AddSayAsTags(Convert.ToDateTime(ma.DueDate).ToString("yyyyMMdd"), InterpretAs.Date, "mdy")));
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