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
            return await GetJsonResult<Me>(Url.Me);
        }

        public async Task<Student> GetStudent(int familyID, string studentFirstName)
        {
            var students = await GetJsonResult<List<StudentBase>>(string.Format(Url.Students, familyID));

            var studentBase = students.FirstOrDefault(s => s.FirstName == studentFirstName);

            return await GetJsonResult<Student>(string.Format(Url.Student, studentBase.ID));         
           
        }

        public async Task<List<MissingAssignment>> GetMissingAssingments(int studentID)
        {
            return await GetJsonResult<List<MissingAssignment>>(string.Format(Url.Missing,studentID));
        }

        private async Task<T> GetJsonResult<T>(string url) where T : new()
        {
            string httpResultString = string.Empty;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.User.AccessToken);

            var httpResponseMessage = await _httpClient.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {                
                httpResultString = await httpResponseMessage.Content.ReadAsStringAsync();                
            }

            httpResponseMessage.Dispose();

            return JsonConvert.DeserializeObject<T>(httpResultString); 
        }
    }
}