using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azure4Alexa.Sycamore.Models;
using AlexaSkillsKit.Speechlet;
using System.Net.Http;
using Azure4Alexa.Sycamore.Interfaces;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using AlexaSkillsKit.Slu;
using Azure4Alexa.Sycamore.Constants;

namespace Azure4Alexa.Sycamore.Data
{
    public partial class Repository : IRepository
    {
        private Session _session;
        private HttpClient _httpClient;        

        public Repository(Session session)
        {
            _session = session;            
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Url.SycamoreBaseUrl);
        }

        public async Task<Me> GetMe()
        {
            var json = await GetJsonResult(Url.Me);
            return JsonConvert.DeserializeObject<Me>(json);
        }

        public async Task<Student> GetStudent(int familyID, string studentFirstName)
        {
            var json = await GetJsonResult(string.Format(Url.Students, familyID));
            var students = JsonConvert.DeserializeObject<List<StudentBase>>(json);           

            var studentBase = students.FirstOrDefault(s => s.FirstName == studentFirstName);
            json = await GetJsonResult(string.Format(Url.Student, studentBase.ID));

            var student = JsonConvert.DeserializeObject<Student>(json);

            return student;
        }

        private async Task<string> GetJsonResult(string url)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.User.AccessToken);
            string httpResultString = string.Empty;
            var httpResponseMessage = await _httpClient.GetAsync(url);
            if (httpResponseMessage.IsSuccessStatusCode)
            {                
                httpResultString = await httpResponseMessage.Content.ReadAsStringAsync();                
            }

            httpResponseMessage.Dispose();
            return httpResultString;
        }
    }
}