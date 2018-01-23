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
    

    public class AccountBalance
    {
       
        
        public static async Task<SpeechletResponse> GetResults(Session session, Intent intent)        
        {
            var repository = DataFactory.Create(session);
            string accountNameFilter = ParseAlias(intent.Slots["accountName"]?.Value);         

            var me = await repository.GetMe();
            var account = await repository.GetAccount(me.FamilyID, accountNameFilter);
            
            return AlexaUtils.BuildSpeechletResponse(ParseResults(account, accountNameFilter),true);

        }

        private static AlexaUtils.SimpleIntentResponse ParseResults(Account account, string accountNameFilter)
        {
            StringBuilder spokenText = new StringBuilder();
            


            if (account == null)
            {
                spokenText.Append(AlexaUtils.AddSentenceTags($"I could not find an account named {accountNameFilter}"));
                spokenText.Append(AlexaUtils.AddSentenceTags($"I can lookup for the balance for accounts named cafeteria or childcare"));
                
            }
            else
            {
                spokenText.Append(AlexaUtils.AddSentenceTags($"The balance on your {account.Name} account is {AlexaUtils.AddSayAsTags("$" + account.Amount, InterpretAs.Unit)}"));                             
            }

            return new AlexaUtils.SimpleIntentResponse()
            {
                ssmlString = AlexaUtils.AddSpeakTags(spokenText.ToString())                
            };
        }

        private static string ParseAlias(string accountName)
        {
            string[] cafeteriaAliases = { "hot lunch", "lunch" };
            string[] childcareAliases = { "before school care", "after school care" };
            
            if (cafeteriaAliases.Contains(accountName))
            {
                return "cafeteria";
            }
            else if (childcareAliases.Contains(accountName))
            {
                return "childcare";
            }
            else
            {
                return accountName;
            }            
        }
    }
}