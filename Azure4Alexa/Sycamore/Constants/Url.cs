using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azure4Alexa.Sycamore.Constants
{
    public class Url
    {
        public static string SycamoreBaseUrl = "https://app.sycamoreeducation.com/api/v1";
        public static string Me = "/Me";
        public static string Students = "/Family/{0}/Students";
        public static string Student = "/Student/{0}";
        public static string StudentPhoto = "https://app.sycamoreeducation.com/Schools/{0}/Students";
    }
}