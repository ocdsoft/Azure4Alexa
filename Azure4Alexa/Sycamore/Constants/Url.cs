﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azure4Alexa.Sycamore.Constants
{
    public class Url
    {
        public static string SycamoreBaseUrl = "https://app.sycamoreeducation.com/api/v1";
        public static string Me = "/Me";
        // familyID
        public static string Students = "/Family/{0}/Students";
        // studentID
        public static string Student = "/Student/{0}";
        // schoolID, picture
        public static string StudentPhoto = "https://app.sycamoreeducation.com/Schools/{0}/Students/{1}";
        // studentID
        public static string Missing = "/Student/{0}/Missing";
        public static string Logo = "https://app.sycamoreschool.com/images/ss_logo_red.png";
        // studentID
        public static string Homework = "/Student/{0}/Homework";
        // schoolID, yyyy-MM-dd
        public static string Menu = "/School/{0}/Cafeteria?start={1}&end={1}";

        // familyID
        public static string AccountBalance = "/Family/{0}/Accounts";
    }
}