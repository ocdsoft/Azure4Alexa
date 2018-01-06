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
    }
}
