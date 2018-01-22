using AlexaSkillsKit.Speechlet;
using Azure4Alexa.Sycamore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure4Alexa.Sycamore.Interfaces
{
    public interface IRepository
    {
        Task<Me> GetMe();
        Task<Student> GetStudent(int familyID, string studentFirstName);
        Task<List<MissingAssignment>> GetMissingAssingments(int studentID, DateTime dateFilter);
        Task<List<HomeworkAssignment>> GetHomeworkAssingments(int studentID, DateTime dateFilter);
        Task<MenuItem> GetMenuItem(int schoolID, DateTime dateFilter);
        Task<Account> GetAccount(int familyID, string accountName);

    }
}
