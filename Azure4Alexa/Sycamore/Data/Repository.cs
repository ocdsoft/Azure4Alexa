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
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.User.AccessToken);
            Me me = new Me();
            var httpResponseMessage = await _httpClient.GetAsync(Url.Me);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                //httpResultString = httpResponseMessage.Content.ReadAsStringAsync().Result;
                var httpResultString = await httpResponseMessage.Content.ReadAsStringAsync();
                me = JsonConvert.DeserializeObject<Me>(httpResultString);                
            }
            
            httpResponseMessage.Dispose();
            return me;
            
        }

        public async Task<Student> GetStudent(int familyID, string studentFirstName)
        {           
            IList<StudentBase> students = new List<StudentBase>();
            Student student = new Student();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.User.AccessToken);
            
            var httpResponseMessage = await _httpClient.GetAsync(string.Format(Url.Students,familyID));
            if (httpResponseMessage.IsSuccessStatusCode)
            {                
                var httpResultString = await httpResponseMessage.Content.ReadAsStringAsync();
                students = JsonConvert.DeserializeObject<List<StudentBase>>(httpResultString);                
            }
            httpResponseMessage.Dispose();

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.User.AccessToken);

            var studentBase = students.FirstOrDefault(s => s.FirstName == studentFirstName);
            httpResponseMessage = await _httpClient.GetAsync(string.Format(Url.Students, familyID));
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var httpResultString = await httpResponseMessage.Content.ReadAsStringAsync();
                student = JsonConvert.DeserializeObject<Student>(httpResultString);
            }
            httpResponseMessage.Dispose();


            return student;

        }
    }
}