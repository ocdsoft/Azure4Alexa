using AlexaSkillsKit.Slu;
using AlexaSkillsKit.Speechlet;
using Azure4Alexa.Sycamore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Azure4Alexa.Sycamore.Data
{
    public class DataFactory
    {
        public static IRepository Create(Session session)
        {
            return new Repository(session);
        }
    }
}